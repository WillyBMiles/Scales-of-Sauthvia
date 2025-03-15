using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitSpawn : Ability
{
    public int number;

    public GameObject projectile;
    public bool specialAttack;
    public Vector3 size;
    public int damage;

    protected override void OnGotHit()
    {
        base.OnGotHit();

        Projectile p = playerController.Attack(true, specialAttack, projectile);
        p.transform.localScale = size;
        p.damage = damage;
    }
}
