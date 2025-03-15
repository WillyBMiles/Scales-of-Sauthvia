using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFireball : Ability
{
    public int number;
    public Sprite sprite;

    public override void OnAttack(Projectile projectile, bool special)
    {
        base.OnAttack(projectile, special);
        if (!special && CheckCount(number))
        {
            projectile.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            projectile.LocalHit += Slow;
        }
    }
    void Slow(Character character)
    {
        var eb = character.GetComponent<EnemyBehavior>();
        eb.speed *= .5f;
    }
}
