using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    public float moveSpeed;
    [SerializeField] float speedSmooth;
    [SerializeField] float minY;
    [SerializeField] float minX;
    [SerializeField] float maxY;
    [SerializeField] float maxX;
    [SerializeField] Sprite wingsUp;
    [SerializeField] Sprite wingsDown;
    [SerializeField] SpriteRenderer dragonSprite;
    [SerializeField] float dampFlapSpeed;
    [SerializeField] Sprite wingsUpBreath;
    [SerializeField] Sprite wingsDownBreath;

    [SerializeField] float negativeFlapSpeed;
    [SerializeField] float neutralFlapSpeed;
    [SerializeField] float positiveFlapSpeed;

    [SerializeField] float AngleDif;
    [SerializeField] float angleSpeed;

    [SerializeField] Transform fireSpawn;
    [SerializeField] float attackCd;

    public Action<Projectile, bool> OnLaunchAttack;
    Sprite currentWing1;
    Sprite currentWing2;
    int wingSituation = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentWing1 = wingsUp;
        currentWing2 = wingsDown;
        x = transform.position.x;
        y = transform.position.y;
    }
    Vector2 input;
    float y;
    float x;
    Vector2 velocity;

    float beatingSpeed = 1f;
    float currentAngle = 0f;

    float nextBeatTime = 0f;
    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 targetVelocity = (input.magnitude > 1 ? input.normalized : input) * moveSpeed;
        velocity = Vector2.Lerp(velocity, targetVelocity, RealTime.deltaTime * speedSmooth);

        y = Mathf.Clamp(y + velocity.y * RealTime.deltaTime, minY, maxY);
        x = Mathf.Clamp(x + velocity.x * RealTime.deltaTime, minX, maxX);
        transform.position = new Vector3(x, y);

        float targetBeat;
        if (input.y > 0f)
        {
            targetBeat = Mathf.Lerp(neutralFlapSpeed, positiveFlapSpeed, input.y);
        }
        else
        {
            targetBeat = Mathf.Lerp(neutralFlapSpeed, negativeFlapSpeed, -input.y);
        }


        beatingSpeed = Mathf.Lerp(beatingSpeed, targetBeat, dampFlapSpeed * RealTime.deltaTime);

        nextBeatTime += Time.deltaTime * beatingSpeed;

        if (nextBeatTime > 1f)
        {
            nextBeatTime -= 1f;
            wingSituation = wingSituation == 0 ? 1 : 0;
            UpdateSpritesNow();
        }


        float targetAngle = (-input.x + input.y) * AngleDif;
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, angleSpeed * RealTime.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));

        if (attackHeld)
            Attack(false, false, projectile);
    }
    
    public void OnMovement(InputAction.CallbackContext value)
    {
        input = value.ReadValue<Vector2>();
    }

    bool attackHeld;
    Sequence breathSequence;
    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Attack(true, false, projectile);
        }
        attackHeld = value.ReadValueAsButton();
            

    }
    float cdTime;
    float timeHit = -1;
    public Projectile Attack(bool overrideChecks, bool specialAttack, GameObject projectile)
    {
        if (!overrideChecks && RealTime.time < cdTime)
            return null;
        if (RealTime.Paused)
            return null;
        cdTime = RealTime.time + attackCd;
        Projectile p = Instantiate(projectile, fireSpawn.position, projectile.transform.rotation).GetComponent<Projectile>();
        OnLaunchAttack?.Invoke(p, specialAttack);

        currentWing1 = wingsUpBreath;
        currentWing2 = wingsDownBreath;
        UpdateSpritesNow();
        breathSequence.Kill();
        breathSequence = DOTween.Sequence();
        breathSequence.AppendInterval(.2f);
        breathSequence.AppendCallback(() =>
        {
            currentWing1 = wingsUp;
            currentWing2 = wingsDown;
            UpdateSpritesNow();
        });
        
      
        return p;
    }


    void UpdateSpritesNow()
    {
        dragonSprite.sprite = wingSituation == 1 ? currentWing1 : currentWing2;
    }
}
