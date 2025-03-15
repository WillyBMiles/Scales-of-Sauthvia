using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class ToggleSound : MonoBehaviour
{
    public bool on = true;

    public Image image;
    public Sprite onSprite;
    public Sprite offSprite;
    public AudioMixer mixer;

    static bool instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == true)
        {
            Destroy(gameObject);
            return;
        }
            
        instance = true;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = on ? onSprite : offSprite;
        if (on)
        {
            AudioMixerSnapshot[] snapshots = { mixer.FindSnapshot("Playing") };
            float[] times = { 1f };
            mixer.TransitionToSnapshots(snapshots, times , 0f);
        }
        else
        {
            AudioMixerSnapshot[] snapshots = { mixer.FindSnapshot("Muted") };
            float[] times = { 1f };
            mixer.TransitionToSnapshots(snapshots, times, 0f);
        }
    }

    public void Click()
    {
        on = !on;
    }
}
