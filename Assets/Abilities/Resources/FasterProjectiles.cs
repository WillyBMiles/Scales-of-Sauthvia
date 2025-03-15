using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterProjectiles : Ability
{
    public override void OnAttack(Projectile projectile, bool special)
    {
        base.OnAttack(projectile, special);
        if (!special)
            projectile.speed *= 1.5f;
    }
}
