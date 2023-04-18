using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(speed, speed, 0, Space.World);
    }
}
