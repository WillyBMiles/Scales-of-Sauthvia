using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandom : MonoBehaviour
{
    public List<AudioClip> clips = new();

    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        source = GetComponent<AudioSource>();
        source.clip = clips.RandomValue();
        source.Play();
        
        Invoke(nameof(TryDestroy), 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TryDestroy()
    {
        if (gameObject == null)
            return;
        Destroy(gameObject);
    }
}
