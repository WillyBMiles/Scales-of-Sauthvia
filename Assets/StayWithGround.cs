using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StayWithGround : MonoBehaviour
{
    public ContactFilter2D layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    List<RaycastHit2D> hits = new();
    // Update is called once per frame
    void Update()
    {
        hits.Clear();
        if (Physics2D.Raycast(transform.position + Vector3.up * 5f,Vector2.down, layerMask, hits) > 0)
        {
            RaycastHit2D hit = hits.OrderBy(hit => hit.point.y).Reverse().First();
            transform.SetY(hit.point.y);
        }
    }
}
