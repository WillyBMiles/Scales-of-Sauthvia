using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflection : Ability
{
    public override void OnAttack(Projectile projectile, bool special)
    {
        base.OnAttack(projectile, special);
        if (Random.value < .25)
        {
            projectile.collapse = true;
        }
    }
}
