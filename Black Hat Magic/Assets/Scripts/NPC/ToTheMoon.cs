using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTheMoon : MonoBehaviour
{

    private float moveSpeed = 2f;
    private float yPointDisappear = 1.25f;
    private float disappearRate = 3.5f;

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {   // fly upwards
        if (transform.position.y <= yPointDisappear)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        } else if (sprite.color.a == 0) // destroy object when it fades
        {
            Destroy(this);
        } else // decrease opacity until it fades
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - disappearRate * Time.deltaTime);
        }
    }
}
