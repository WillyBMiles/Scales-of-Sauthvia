using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormationBehavior : MonoBehaviour
{
    Formation formation;
    public enum Movement { 
        Strafe,
    }

    public Movement state;
    Vector3 startingPosition;
    IEnumerable<EnemyBehavior> enemiesInFormation => formation.enemyBehaviors.Where(eb => eb.InFormation);
    EnemyBehavior highestEnemy => enemiesInFormation.OrderBy(eb => eb.transform.position.y).Reverse().FirstOrDefault();
    EnemyBehavior lowestEnemy => enemiesInFormation.OrderBy(eb => eb.transform.position.y).FirstOrDefault();

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        formation = GetComponentInParent<Formation>();
    }

    // Update is called once per frame
    void Update()
    {


        switch (state){
            case Movement.Strafe:
                Strafe();
                break;
        }
    }

    public float moveSpeed = 2f;
    public float maxOffset = 3f;
    bool movingUp;
    public void Strafe()
    {
        if (highestEnemy == null)
            return;

        if (startingPosition.y + maxOffset < highestEnemy.transform.position.y && movingUp)
        {
            movingUp = false;
        }
        if (startingPosition.y - maxOffset > lowestEnemy.transform.position.y && !movingUp)
            movingUp = true;
        if (movingUp)
            transform.SetY(moveSpeed * RealTime.deltaTime, true);
        else
            transform.SetY(-moveSpeed * RealTime.deltaTime, true);
    }

}
