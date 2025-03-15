using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupOfEnemies : MonoBehaviour
{

    public List<Formation> possibleFormations = new();
    List<Formation> currentFormationPossibilities = new();
    public int OverrideNumber = 0;
    public int NumberOfFormations = -1;
    List<Formation> currentFormations = new();

    public Formation spawnThisOneToo;

    public int OverrideSpawnThisNumber = -1;

    public bool useAll = true;

    public List<GameObject> possibleWalkingEnemies = new();
    List<GameObject> currentWalkingEnemyPossibilities = new();
    List<GameObject> currentWalkingEnemies = new();

    public float maxGapBetweenWalkingEnemies;
    public float minGapBetweenWalkingEnemies;

    public int NumberOfWalkingEnemies;

    public bool LinkWalkingEnemiesWithFormations;

    public bool IsDead => NumberOfWalkingEnemies == 0 && NumberOfFormations == 0 && currentFormations.Count == 0 && currentWalkingEnemies.Count == 0;

    // Start is called before the first frame update
    void Start()
    {
        if (LinkWalkingEnemiesWithFormations)
        {
            numberOfWalkingEnemiesPerFormation = (float)NumberOfWalkingEnemies / NumberOfFormations;
        }
        SpawnNewFormation();

        if (spawnThisOneToo)
        {

            spawnThisOneToo = Instantiate(spawnThisOneToo, Vector3.zero, Quaternion.identity);
            if (OverrideSpawnThisNumber > 0)
            {
                spawnThisOneToo.Number = OverrideSpawnThisNumber;
            }
            spawnThisOneToo.Initiate();
            currentFormations.Add(spawnThisOneToo);

            spawnThisOneToo.transform.position += Vector3.left * 5f;
        }

        
    }

    private void OnDestroy()
    {
        if (spawnThisOneToo != null)
            Destroy(spawnThisOneToo.gameObject);
    }

    float gap = 0f;
    // Update is called once per frame
    void Update()
    {
        if (currentFormations.Count == 0 && (!LinkWalkingEnemiesWithFormations || (walkingEnemiesToSpawn <= 0 && currentWalkingEnemies.Count == 0)))
        {
            SpawnNewFormation();
        }

        foreach (var formation in currentFormations.ToArray())
        {
            if (formation.FormationIsDead)
            {
                currentFormations.Remove(formation);
                Destroy(formation.gameObject);
            }
        }
        foreach (var enemy in currentWalkingEnemies.ToArray())
        {
            if (enemy == null)
            {
                currentWalkingEnemies.Remove(enemy);
            }
        }

        if (gap <= 0 && walkingEnemiesToSpawn > 0)
        {
            SpawnWalkingEnemy();
            walkingEnemiesToSpawn--;
            gap = Random.Range(minGapBetweenWalkingEnemies, maxGapBetweenWalkingEnemies);
        }
        gap -= Time.deltaTime;

        if (NumberOfFormations == 0)
        {
            walkingEnemiesToSpawn = NumberOfWalkingEnemies;
        }  
    }
    int walkingEnemiesToSpawn;
    float numberOfWalkingEnemiesPerFormation;
    void SpawnNewFormation()
    {
        if (NumberOfFormations <= 0)
            return;
        RefillPossibilities();
        Formation prefab = currentFormationPossibilities.RandomValue();
        Formation newFormation = Instantiate(prefab, new Vector3(), Quaternion.identity);
        if (OverrideNumber > 0)
            newFormation.Number = OverrideNumber;
        currentFormations.Add(newFormation);
        newFormation.Initiate();
        if (useAll)
        {
            currentFormationPossibilities.Remove(prefab);
        }
        
        if (LinkWalkingEnemiesWithFormations)
        {
            walkingEnemiesToSpawn = (int) numberOfWalkingEnemiesPerFormation;
        }
        NumberOfFormations--;
    }

    void RefillPossibilities()
    {
        if (currentFormationPossibilities.Count == 0)
            currentFormationPossibilities.AddRange(possibleFormations);
        if (currentWalkingEnemyPossibilities.Count == 0)
            currentWalkingEnemyPossibilities.AddRange(possibleWalkingEnemies);
    }

    public void SpawnWalkingEnemy()
    {
        if (NumberOfWalkingEnemies <= 0)
            return;

        RefillPossibilities();
        GameObject prefab = currentWalkingEnemyPossibilities.RandomValue();
        Transform position = EnemySpawner.Instance.walkingEnemySpawnLocation.transform;
        GameObject walkingEnemy = Instantiate(prefab, position.position, position.rotation);

        currentWalkingEnemies.Add(walkingEnemy);

        NumberOfWalkingEnemies--;
    }

    
}
