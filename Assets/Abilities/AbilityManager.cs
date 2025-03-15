using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] GameObject levelUpUI;
    [SerializeField] PlayerController playerController;

    [SerializeField] List<Ability> abilities;

    readonly List<Ability> addedAbilities = new();
    List<Ability> PossibleNewAbilities => abilities.Where(ability => !addedAbilities.Contains(ability)).Where(ability =>
        addedAbilities.Where(a => a.exclusiveGroup != Ability.ExclusiveGroup.None && a.exclusiveGroup == ability.exclusiveGroup).Count() == 0
    ).ToList();

    List<Ability> options;
    public List<LevelUpButton> levelUpButtons;

    public int numberOfEnemiesDie = 10;
    int numberOfEnemies = 0;

    public float sizePerLevel;
    int numberOfLevelsPerHP = 1;

    public int healPerLevel = 1;
    int numLevels = 0;

    // Start is called before the first frame update
    void Start()
    {
        Character.OnDie += OnCharacterDie;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfEnemies > numberOfEnemiesDie)
        {
            LevelUp();
        }
    }

    public void OnCharacterDie(Character character)
    {
        //TODO
        numberOfEnemies ++;
    }

    private void OnDestroy()
    {
        Character.OnDie -= OnCharacterDie;
    }

    public void  LevelUp()
    {
        playerController.transform.localScale = playerController.transform.localScale + new Vector3(sizePerLevel, sizePerLevel, sizePerLevel);
        GetLineup();
        RealTime.Paused = true;
        numberOfEnemiesDie = (int) ( numberOfEnemiesDie * 1.1f);
        numberOfEnemies = 0;
        var player = playerController.GetComponent<Player>();
        numLevels++;
        if (numLevels >= numberOfLevelsPerHP)
        {
            
            player.maxHealth++;
            numberOfLevelsPerHP++;
             
            numLevels = 0;
        }
        player.health += healPerLevel;
        if (player.health > player.maxHealth)
            player.health = player.maxHealth;

    }

    public void GetLineup()
    {
        cleared = false;
        levelUpUI.SetActive(true);
        options = PossibleNewAbilities.RandomValues(3);
         for (int i = 0; i < options.Count; i++)
        {
            levelUpButtons[i].SetAbility(options[i]);
            levelUpButtons[i].FadeIn();
        }

    }

    public void AddAbility(int id)
    {
        if (cleared)
            return;
        AddAbility(options[id]);
        ClearLineUp();
    }

    bool cleared = false;
    void ClearLineUp()
    {
        cleared = true;
        foreach (var item in levelUpButtons)
        {
            item.FadeOut();
        }
        UnPause();
    }

    void UnPause()
    {
        RealTime.Paused = false;
        levelUpUI.SetActive(false);

    }

    void AddAbility(Ability ability)
    {
        Ability newAbility = Instantiate(ability, playerController.transform);

        newAbility.Add();
        if (!ability.repeatable)
            addedAbilities.Add(ability);
    }

#if UNITY_EDITOR
    [Button]
    void GatherAbilities()
    {
        abilities = Resources.FindObjectsOfTypeAll(typeof(Ability)).Select(obj => obj as Ability).ToList();
    }
#endif
}
