using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : Ability
{
    public int number;
    public float shrinkSize;

    Sequence sequence;
    protected override void OnGotHit()
    {
        base.OnGotHit();
        if (CheckCount(number))
        {
            playerController.transform.localScale *= shrinkSize;

            sequence = DOTween.Sequence();
            sequence.AppendInterval(2.5f);
            sequence.AppendCallback(() => playerController.transform.localScale /= shrinkSize);
        }
    }

    private void OnDestroy()
    {
        sequence?.Kill();
    }

}
