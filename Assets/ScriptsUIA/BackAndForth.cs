using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public int speed = 3;
    public float maxZ = 16.0f;
    public float minZ = -16.0f;

    private int _direction = 1;
    private bool bounced = false;
            
    void Update()
    {
        transform.Translate(0, 0, _direction * speed * Time.deltaTime);

        if(transform.position.z > maxZ || transform.position.z < minZ)
        {
            _direction = -_direction;
            bounced = true;
        }
        if(bounced) 
        {
            transform.Translate(0, 0, _direction * speed * Time.deltaTime);
        }
    }
}
