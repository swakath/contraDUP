using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonFollow : MonoBehaviour
{
    private new Transform camera;
    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").transform;
      
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tempPos = transform.position;
        tempPos.x = camera.position.x - 6.82f;
        transform.position = tempPos;
    }
}
