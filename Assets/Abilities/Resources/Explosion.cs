using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Ability
{
    public GameObject explosions;
    public override void OnAttack(Projectile projectile, bool special)
    {
        base.OnAttack(projectile, special);
        if (CheckCount(5))
            projectile.explosions.Add(explosions);
    }
}
