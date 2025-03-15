using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleSeeder : MonoBehaviour
{
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.SetFloat("_Seed", Random.Range(-1000f, 1000f));
    }

}
