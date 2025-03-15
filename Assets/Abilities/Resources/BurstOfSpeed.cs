using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstOfSpeed : Ability
{
    public int number;
    public float moveSpeed;
    Sequence sequence;

    public override void OnAttack(Projectile projectile, bool special)
    {
        base.OnAttack(projectile, special);
        if (special)
        {
            if (CheckCount(number))
            {
                playerController.moveSpeed *= moveSpeed;
                sequence = DOTween.Sequence();
                sequence.AppendInterval(1f);
                sequence.AppendCallback(() => { playerController.moveSpeed /= moveSpeed; });
            }
        }
    }

    private void OnDestroy()
    {
        sequence?.Kill();
    }
}
