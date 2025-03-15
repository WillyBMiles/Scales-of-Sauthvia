using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpyDamage : MonoBehaviour
{
    public Character character;
    public float cooldown = 1f;
    float cd;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {

        cd -= RealTime.deltaTime;
    }

    public static System.Action<BumpyDamage> OnHit;
    public static bool shouldHitPlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        shouldHitPlayer = true;
        if (cd <= 0f && collision.collider.TryGetComponent<Player>(out Player player))
        {
            OnHit?.Invoke(this);
            if (shouldHitPlayer)
                player.TakeDamage(1, transform.position);
            character.TakeDamage(1, transform.position);
            cd = cooldown;

            
        }
    }
}
