using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
        Ability.playerController.OnLaunchAttack += Check;
    }

    private void OnDestroy()
    {
        Ability.playerController.OnLaunchAttack -= Check;
    }
    void Check(Projectile p, bool b)
    {
        time -= 1;
    }

    float time = 10f;
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
