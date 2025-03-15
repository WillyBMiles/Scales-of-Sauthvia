using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrange : MonoBehaviour
{
    public float spacing;
    public bool vertical;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    void ArrangeChildren()
    {
        int i = 0;
        foreach (Transform subTransform in transform)
        {
            subTransform.localPosition = new Vector3(vertical ? 0f : spacing * i, vertical ?  spacing * i : 0f);
            i++;
        }
    }
}
