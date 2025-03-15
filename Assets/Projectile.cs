using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool BelongsToThePlayer;
    public Vector2 speed;
    public int damage = 1;

    public bool collapse;
    public bool dontCollapseMe;
    public GameObject hitSpawn;
    public bool dontDestroy;
    public bool oncePerEnemy;
    public List<GameObject> explosions = new();

    bool hit = false;

    List<Character> hits = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3) speed * RealTime.deltaTime;
        if (Mathf.Abs(transform.position.x) > 12f)
            Destroy(gameObject);
    }

    public static System.Action<Projectile> Hit;
    public System.Action<Character> LocalHit;
    public static bool ShouldHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hit)
            return;

        ShouldHit = true;
        Character c = collision.collider.GetComponent<Character>();
        if (c!= null)
        {
            if (dontCollapseMe)
                return;
            if (hits.Contains(c))
                return;
            if (c.isPlayer != BelongsToThePlayer)
            {

                hits.Add(c);
                LocalHit?.Invoke(c);
                Hit?.Invoke(this);
                if (!ShouldHit)
                    return;

                c.TakeDamage(damage, collision.GetContact(0).point);
                if (!dontDestroy)
                {
                    Destroy(gameObject);
                    foreach (var item in explosions)
                    {
                        Instantiate(item, transform.position, transform.rotation);
                    }
                }
                if (!oncePerEnemy)
                    hit = true;
            }
        }

        if (collapse)
        {
            Projectile p = collision.collider.GetComponent<Projectile>();

            if (p != null && p.BelongsToThePlayer != BelongsToThePlayer)
            {
                if (p.dontDestroy)
                    return;
                Destroy(p.gameObject);
                if (!dontCollapseMe)
                {
                    Destroy(gameObject);
                    foreach (var item in explosions)
                    {
                        var explosion = Instantiate(item, transform.position, transform.rotation);
                        explosion.GetComponent<Projectile>().hits.AddRange(hits);
                    }
                }
                    
                Instantiate(hitSpawn, transform.position, transform.rotation);
            }

        }

    }
}
