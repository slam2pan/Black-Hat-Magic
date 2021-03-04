using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSignBounce : MonoBehaviour
{
    private float min = 2f;
    private float max = 3f;
    private float speed = 50f;

    void Awake()
    {
        min = transform.position.x;
        max = transform.position.x + 40;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time*speed, max - min) + min, transform.position.y, transform.position.z);
    }
}
