using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRight : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float rightEdge = 10f;

    void Update()
    {
       transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

       if (transform.position.x > rightEdge) {
           Destroy(this.gameObject);
       }
    }
}
