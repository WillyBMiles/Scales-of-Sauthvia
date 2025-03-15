using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeProjectiles : Ability
{
    protected override void OnProjectileHit(Projectile projectile)
    {
        base.OnProjectileHit(projectile);
        if (Random.value < .1f)
        {
            Projectile.ShouldHit = false;
        }
    }
}
