using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSize : Ability
{
    protected override void OnAdd()
    {
        base.OnAdd();
        playerController.transform.localScale += new Vector3(.4f, .4f, .4f);
        player.maxHealth += 4;
        player.health += 4;
    }

}
