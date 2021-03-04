using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyLeft : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float leftEdge = -10f;

    void Update()
    {
       transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

       if (transform.position.x < leftEdge) {
           Destroy(this.gameObject);
       }
    }

    public void ChangeSpeed(float value)
    {
        moveSpeed = value;
    }
}
