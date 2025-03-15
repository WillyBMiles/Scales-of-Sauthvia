using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public int maxHealth;
    public static Player Instance;
    private void Awake()
    {
        Instance = this;
        maxHealth = health;
    }

    protected override void Die()
    {
        Debug.Log("Oops dead");
        SceneManager.LoadScene("EndScreen");
    }
}
