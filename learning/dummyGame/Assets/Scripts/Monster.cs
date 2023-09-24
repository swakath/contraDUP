using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rigBody;
    // Start is called before the first frame update
    void Awake()
    {
        rigBody = GetComponent<Rigidbody2D>();
        speed = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigBody.velocity = new Vector2 (speed, rigBody.velocity.y);
    }
}
