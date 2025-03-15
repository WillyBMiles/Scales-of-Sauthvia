using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyBehavior : MonoBehaviour
{
    public float speed;
    bool entering = true;
    float velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    const float leftEdge = -6;
    const float rightEdge = 8.5f;
    public float wanderTime;
    // Update is called once per frame
    void Update()
    {
        if (entering)
        {
            velocity = -speed;
            if (transform.position.x < 7)
            {
                entering = false;
                Invoke(nameof(Switch), wanderTime * Random.value);
            }
                
           
        }

        transform.SetX(velocity * RealTime.deltaTime, true);
        if (transform.position.x < leftEdge)
        {
            velocity = speed;
        }
        if (transform.position.x > rightEdge)
        {
            velocity = -speed;
        }

    }

    void Switch()
    {
        if (gameObject == null)
            return;

        velocity = -velocity;
        Invoke(nameof(Switch), Random.value * wanderTime);
    }
}
