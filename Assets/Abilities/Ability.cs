using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string DisplayName;
    public string DisplayDescription;
    public bool repeatable;
    protected static Player player { get
        {
            if (p == null)
            {
                p = FindObjectOfType<Player>();
            }
            return p;
        } }
    static Player p;

    public static PlayerController playerController { get
        {
            if (pc == null)
            {
                pc = FindObjectOfType<PlayerController>();
            }
            return pc;
        } }
    static PlayerController pc;

    public enum ExclusiveGroup
    {
        None,
    }

    public ExclusiveGroup exclusiveGroup;

    protected int count = -1;
    protected bool CheckCount(int number)
    {
        if (count == -1)
        {
            count = Random.Range(0, number);
        }

        count++;
        if (count >= number)
        {
            count = 0;
            return true;
        }
        return false;
    }

    protected virtual void OnAdd()
    {
        playerController.OnLaunchAttack += OnAttack;
        Projectile.Hit += OnProjectileHit;
        player.OnHit += OnGotHit;
    }

    protected virtual void OnProjectileHit(Projectile projectile)
    {

    }

    protected virtual void OnGotHit()
    {

    }

    public void Add()
    {

        OnAdd();
    }

    private void OnDisable()
    {
        Projectile.Hit -= OnProjectileHit;
    }

    public virtual void OnAttack(Projectile projectile, bool special)
    {

    }

}
