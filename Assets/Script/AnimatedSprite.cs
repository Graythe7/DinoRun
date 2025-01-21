using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    // instead of repeating the same animation code, we'll use a single script
    public Sprite[] sprite;
    private SpriteRenderer spriteRenderer;
    private int frame; // to keep track of the current frame I am in 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;

        if(frame >= sprite.Length)
        {
            frame = 0;
        }
        spriteRenderer.sprite = sprite[frame];

        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }

}
