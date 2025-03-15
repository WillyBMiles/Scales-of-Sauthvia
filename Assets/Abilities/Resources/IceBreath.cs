using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreath : Ability
{
    public int number;
    public GameObject spawn;

    public override void OnAttack(Projectile projectile, bool special)
    {
        base.OnAttack(projectile, special);
        if (special)
            return;
        if (CheckCount(number))
        {
            Projectile p = playerController.Attack(true, true, spawn);
            p.LocalHit += Slow;
        }
    }

    void Slow(Character character)
    {
        var eb = character.GetComponent<EnemyBehavior>();
        eb.speed *= .2f;
    }
}
