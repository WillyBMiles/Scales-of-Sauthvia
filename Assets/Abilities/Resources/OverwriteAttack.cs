using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverwriteAttack : Ability
{
    public int number;
    public GameObject newAttack;

    public override void OnAttack(Projectile projectile, bool special)
    {
        base.OnAttack(projectile, special);
        if (!special)
        {
            if (CheckCount(number))
            {
                playerController.Attack(true, true, newAttack);
            }
        }
    }
}
