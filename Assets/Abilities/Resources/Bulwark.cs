using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulwark : Ability
{

    protected override void OnAdd()
    {
        base.OnAdd();
        BumpyDamage.OnHit += OnHit;
    }

    void OnHit(BumpyDamage bp)
    {
        if (Random.value < .5f)
        {
            BumpyDamage.shouldHitPlayer = false;
        }
    }

    private void OnDestroy()
    {
        BumpyDamage.OnHit -= OnHit;
    }
}
