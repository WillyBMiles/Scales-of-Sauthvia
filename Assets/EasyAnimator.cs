using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class EasyAnimator : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    [SerializeField] float framesPerSecond;
    [SerializeField] bool loop;
    [SerializeField] bool destroyAtEnd;
    [SerializeField] bool destroyParentToo;

    SpriteRenderer spriteRenderer;

    Sequence sequence;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        MakeNewSequence();

            
    }

    // Update is called once per frame
    void Update()
    {
        if (RealTime.Paused)
        {
            sequence.Pause();
        }
        else
        {
            sequence.Play();
        }
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }

    public void Stop()
    {
        sequence.Kill();
    }
    public void Restart()
    {
        sequence.Kill();
        MakeNewSequence();
    }

    int i = 0;
    void MakeNewSequence()
    {
        spriteRenderer.sprite = sprites[0];
        sequence = DOTween.Sequence();
        sprites.ToList().Skip(1).ToList().ForEach(sprite =>
        {
            sequence.AppendInterval(1f / framesPerSecond);
            sequence.AppendCallback(() => spriteRenderer.sprite = sprite);
            
        });



        if (loop)
        {
            sequence.AppendInterval(1f / framesPerSecond);
            sequence.AppendCallback(() => Restart());
        }
        else if (destroyAtEnd)
        {
            sequence.AppendInterval(1f / framesPerSecond);
            sequence.AppendCallback(() => Destroy(gameObject));
            if (destroyParentToo)
            {
                sequence.AppendCallback(() => Destroy(transform.parent.gameObject));
            }
        }
    }
}
