using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnim : MonoBehaviour
{
    protected virtual float MoveSpeed {get {return 1f;}}
    protected virtual float YDistance {get {return 0.5f;}}
    protected float yPointDisappear;
    protected float appearRate = 3.5f;
    protected float disappearTime = 0.25f;

    protected SpriteRenderer sprite;

    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        yPointDisappear = transform.position.y + YDistance;
    }

    protected virtual void Update()
    {   // fly upwards
        if (transform.position.y < yPointDisappear)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + appearRate * Time.deltaTime);
            transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime);
        } else
        {
            StartCoroutine(DestroySequence());
        }
    }

    private IEnumerator DestroySequence()
    {
        yield return new WaitForSeconds(disappearTime);
        Destroy(this.gameObject);
    }
}
