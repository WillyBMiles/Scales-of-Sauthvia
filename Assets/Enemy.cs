using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public GameObject DeathSpawn;
    public float DeathSize;
    public float PointsWorth;

    protected override void Die()
    {
        Destroy(gameObject);
        GameObject go = Instantiate(DeathSpawn, transform.position, transform.rotation);
        go.transform.localScale = new Vector3(DeathSize, DeathSize, DeathSize);

        EnemySpawner.pointsStatic += PointsWorth;
    }


}
