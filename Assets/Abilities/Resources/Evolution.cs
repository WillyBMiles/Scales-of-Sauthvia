using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : Ability
{
    AbilityManager abilityManager => FindAnyObjectByType<AbilityManager>();
    protected override void OnAdd()
    {
        base.OnAdd();
        abilityManager.healPerLevel++;
    }
}
