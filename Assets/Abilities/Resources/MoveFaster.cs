using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFaster : Ability
{
    protected override void OnAdd()
    {
        base.OnAdd();
        playerController.moveSpeed += 4.5f * 1.35f;
    }
}
