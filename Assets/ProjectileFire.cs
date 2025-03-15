using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    public Projectile toFire;

    public float fireRate;
    float timer;

    public bool facePlayer;
    public Transform spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        timer = fireRate * Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -4f)
            return;

        timer -= RealTime.deltaTime;
        if (timer <= 0f)
        {
            Fire();
            timer = fireRate;
        }

        if (facePlayer)
        {
            transform.right = (Vector2) Player.Instance.transform.position - (Vector2) transform.position;
        }
    }
    void Fire()
    {
        Projectile p = Instantiate(toFire, spawnLocation.position, transform.rotation);
        p.speed = p.transform.right.normalized * p.speed.magnitude;
    }
}
