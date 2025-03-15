using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTime : MonoBehaviour
{
    public static bool Paused = false;
    public static float deltaTime => Time.deltaTime * (Paused ? 0f : 1f);

    static float currentTime;

    public static float time => currentTime;

    // Start is called before the first frame update
    void Start()
    {
        Paused = false;
        currentTime = 0f;
    }

    private void LateUpdate()
    {
        currentTime += deltaTime;
    }
}
