using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    // Update is called once per frame
    public float speed = 5.0f;
    void Update()
    {
        transform.Rotate(0,speed * Time.deltaTime,0);
    }
}
