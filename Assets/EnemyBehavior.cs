using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyBehavior : SerializedMonoBehaviour
{
    SplineAnimate animate;

    public float speed;
    public enum State
    {
        Entering,
        GoToFormation,
        Charging
    }

    public State state;
    public Formation formation;

    SplineContainer entranceSpline;
    public List<Entrance> possibleEntranceSplines = new();

    public bool InFormation => Vector3.Distance(transform.position, FormationLocation.position) < .15f;

    public struct Entrance
    {
        public SplineContainer splineContainer;
        public Vector2 maxOffset;
        public int startingPlace;
    }

    Transform FormationLocation => formation.FormationPoints[this];
    SplineAnimate splineAnimate;

    public float entranceFireChance;

    public float formationFireChange;
    public float chargeFireRate;
    public float waitToBeInRange = Mathf.Infinity;
    public Transform spawnLocation;
    public GameObject attackPrefab;

    public List<SplineContainer> possibleCharges = new();

    SplineContainer chargeSpline;

    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<SplineAnimate>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Entering:
                Entering();
                break;
            case State.GoToFormation:
                GoToFormation();
                break;
            case State.Charging:
                Charging();
                break;
        }
        if (RealTime.Paused)
        {
            splineAnimate.Pause();
        }
        animate.MaxSpeed = speed;
    }

    float chargeTime = 0f;
    public void Charge()
    {
        var spline = possibleCharges.RandomValue();

        chargeSpline = Instantiate(spline, transform.position, transform.rotation);
        splineAnimate.Container = chargeSpline;
        splineAnimate.Restart(true);
        StartCoroutine(ChargeCoroutine());
        ChangeState(State.Charging);
        chargeTime = 0f;
    }

    IEnumerator ChargeCoroutine()
    {
        while (splineAnimate.NormalizedTime < 1f)
        {
            yield return null;
        }
        ChangeState(State.GoToFormation);
        splineAnimate.Pause();
        Destroy(chargeSpline.gameObject);
    }

    void ChangeState(State destinationState)
    {
        state = destinationState;
        StartState(state);
        splineAnimate.Play();
    }

    private void OnDestroy()
    {
        if (entranceSpline != null)
            Destroy(entranceSpline.gameObject);
        if (chargeSpline != null)
            Destroy(chargeSpline.gameObject);
    }


    void StartState(State state)
    {
        switch (state)
        {
            case State.Entering:
                splineAnimate.Container = entranceSpline;
                splineAnimate.Play();
                StartCoroutine(EntranceCoroutine());

                break;
            default:
                break;
        }
    }

    public bool Fire()
    {
        if (Vector3.Distance(Player.Instance.transform.position, transform.position) > waitToBeInRange)
            return false;
        Instantiate(attackPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
        return true;
    }

    IEnumerator EntranceCoroutine()
    {
        while (splineAnimate.NormalizedTime < 1f)
        {
            yield return null;
        }
        ChangeState(State.GoToFormation);
        splineAnimate.Pause();
        Destroy(entranceSpline.gameObject);
    }

    public void Entering()
    {
        splineAnimate.Play();
        if (fire <= 0f)
        {
            if (Random.value < entranceFireChance)
            {
                if (Fire())
                    fire = .5f;
            }
            else
                fire = .5f;
        }
        fire -= RealTime.deltaTime;
    }

    public void GoToFormation()
    {
        transform.position = Vector2.MoveTowards(transform.position, FormationLocation.position, speed * RealTime.deltaTime);
        if (InFormation)
        {
            if (fire <= 0f)
            {
                if (Random.value < formationFireChange)
                {
                    if (Fire())
                    {
                        fire = 1f;
                    }
                    else
                        fire = .25f;
                }
                else
                    fire = 1f;
            }
            fire -= RealTime.deltaTime;
        }
    }


    float fire;
    public void Charging()
    {
        if (fire <= 0f)
        {
            if (Fire())
                fire = 1f / chargeFireRate;
        }
        fire -= RealTime.deltaTime;
        chargeTime += Time.deltaTime;

        if (chargeTime > 7f)
        {
            ChangeState(State.GoToFormation);
        }
    }


    public void SpawnEnemy(Formation formation)
    {
        formation.AddEnemyBehaviorToFormation(this);

        splineAnimate = GetComponent<SplineAnimate>();

        Entrance entrance = possibleEntranceSplines.RandomValue();
        entranceSpline = Instantiate(entrance.splineContainer, null);
        
        entranceSpline.transform.position = EnemySpawner.Instance.startingSpawnAreas[entrance.startingPlace].transform.position + new Vector3(Random.value * entrance.maxOffset.x,
             Random.value * entrance.maxOffset.y);
        
        ChangeState(State.Entering);
        
        
    }
}
