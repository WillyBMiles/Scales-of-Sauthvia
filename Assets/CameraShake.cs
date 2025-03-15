using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    static CameraShake shake;
    public float fade;

    Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        shake = this;
        start = transform.position;
    }

    float shakeAmount;

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 60;
        shakeAmount = Mathf.Lerp(shakeAmount, 0f, Time.deltaTime * fade);
        transform.position = (Vector3) ((Vector2) start + shakeAmount * Random.insideUnitCircle) + Vector3.forward * transform.position.z;

    }

    public static void Shake(float _shake)
    {
        shake.shakeAmount = _shake;
    }

}
