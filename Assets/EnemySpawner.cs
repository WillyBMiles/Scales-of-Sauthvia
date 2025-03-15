using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public List<GameObject> startingSpawnAreas = new();
    public GameObject walkingEnemySpawnLocation;

    GroupOfEnemies currentGroup;
    public List<GroupOfEnemies> groups = new();

    public int groupNumber;

    public static float pointsStatic;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        pointsStatic = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentGroup == null || currentGroup.IsDead)
        {
            if (currentGroup != null)
            {
                Destroy(currentGroup);
            }
            SpawnNewGroup();

        }
            
    }

    void SpawnNewGroup()
    {
        GroupOfEnemies group = groups[groupNumber];
        currentGroup = Instantiate(group, new Vector3(), Quaternion.identity);

        groupNumber++;
    }
}
