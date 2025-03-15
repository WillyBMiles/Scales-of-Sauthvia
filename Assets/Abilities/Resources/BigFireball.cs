using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFireball : Ability
{

    public override void OnAttack(Projectile projectile, bool special)
    {
        projectile.transform.localScale *= 1.5f;
    }
}
