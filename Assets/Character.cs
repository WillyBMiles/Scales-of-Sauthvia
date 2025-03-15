using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int health;
    public bool isPlayer;
    public GameObject hitMarker;
    public bool showHitMarkerWhileDead;

    public System.Action OnHit;

    public static System.Action<Character> OnDie;

    void InternalDie()
    {
        OnDie?.Invoke(this);
        Die();
    }

    protected abstract void Die();

    public virtual void TakeDamage(int amount, Vector3 location)
    {
        if (!isPlayer && !Visible())
            return;
        OnHit?.Invoke();
        if (isPlayer)
        {
            CameraShake.Shake(.2f);
        }

        health -= amount;

        if (health <= 0)
        {
            if (showHitMarkerWhileDead)
                Instantiate(hitMarker, location, transform.rotation);
            InternalDie();
        }
        else
            Instantiate(hitMarker, location, transform.rotation);

    }

    bool Visible()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        if (pos.x > (Screen.safeArea.xMax))
        {
            return false;
        }
        if (pos.x < Screen.safeArea.xMin)
        {
            return false;
        }
        if (pos.y > Screen.safeArea.yMax)
        {
            return false;
        }
        if (pos.y < Screen.safeArea.yMin)
        {
            return false;
        }
        return true;
    }
}
