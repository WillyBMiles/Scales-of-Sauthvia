using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraDamage : Ability
{
    public override void OnAttack(Projectile projectile, bool special)
    {
        base.OnAttack(projectile, special);
        if (CheckCount(4))
        {
            projectile.transform.localScale *= 1.5f;
            projectile.damage += 1;
        }
        
    }
}
