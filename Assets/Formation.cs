using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class Formation : SerializedMonoBehaviour
{
    public List<EnemyBehavior> enemyBehaviors = new();
    List<EnemyBehavior> chargers => enemyBehaviors.Where(eb => eb.state == EnemyBehavior.State.Charging).ToList();
    readonly Dictionary<EnemyBehavior, Transform> formationPoints = new();
    [SerializeField]
    List<Transform> allFormationPoints;
    List<Transform> AvailableFormationPoints => allFormationPoints.Where(transform => !formationPoints.ContainsValue(transform)).ToList();
    public IReadOnlyDictionary<EnemyBehavior, Transform> FormationPoints => formationPoints;

    public List<GameObject> enemyPrefabs;
    public int Number;
    public float spawnsPerSecond;
    public int spawnsPerSpawn = 1;

    public int charges = 2;
    public float delayCharge = 0f;

    public bool FormationIsDead => Number <= 0 && enemyBehaviors.Count == 0;

    Sequence sequence;
    public void Initiate()
    {
        chargeDelay = 10f;
        sequence = DOTween.Sequence();
        for (int i =0; i < Number; i += spawnsPerSpawn)
        {
            sequence.AppendInterval(1f / spawnsPerSecond);
            for (int j =0; j < spawnsPerSpawn; j++)
            {
                if (i + j > Number)
                    return;

                sequence.AppendCallback(() => {
                    GameObject enemy = Instantiate(enemyPrefabs.RandomValue(), transform.position + Vector3.right * 100f, Quaternion.identity);
                    var eb = enemy.GetComponent<EnemyBehavior>();
                    eb.SpawnEnemy(this);
                    Number--;
                }
);
            }

        }
        sequence.AppendInterval(2f);
        sequence.AppendCallback(() => chargeDelay = delayCharge );
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float chargeDelay = 0f;
    // Update is called once per frame
    void Update()
    {
        if (RealTime.Paused)
        {
            sequence.Pause();
        }
        else
        {
            sequence.Play();
        }

        foreach (var points in formationPoints.ToArray())
        {
            if (points.Key == null)
            {
                formationPoints.Remove(points.Key);
            }
        }

        foreach (var enemyBehavior in enemyBehaviors.ToArray())
        {
            if (enemyBehavior == null)
                enemyBehaviors.Remove(enemyBehavior);
        }

        if (chargers.Count < charges && chargeDelay <= 0f)
        {
            var eb = enemyBehaviors.Where(eb => eb.state != EnemyBehavior.State.Charging && eb.InFormation).RandomValue();
            if (eb != null)
            {
                eb.Charge();
                chargeDelay = delayCharge;
            }
                

        }
        chargeDelay -= RealTime.deltaTime;
    }

    public void AddEnemyBehaviorToFormation(EnemyBehavior enemyBehavior)
    {
        enemyBehaviors.Add(enemyBehavior);
        enemyBehavior.formation = this;
        formationPoints[enemyBehavior] = AvailableFormationPoints.FirstOrDefault();
    }
}


