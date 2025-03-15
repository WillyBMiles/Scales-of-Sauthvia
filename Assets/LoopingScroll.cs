using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingScroll : MonoBehaviour
{
    public float speed;
    public float left;
    public float right;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < left)
            Destroy(gameObject);
        if (transform.position.x > right)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < left)
        {
            transform.position = new Vector3( right, transform.position.y);
        }
        transform.position += Vector3.left * speed * RealTime.deltaTime;
    }

}
