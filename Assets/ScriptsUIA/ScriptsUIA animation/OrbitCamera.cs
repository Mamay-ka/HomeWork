using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;//—ериализованна€ ссылка на объект, вокруг которого производитс€ облет

    public float rotSpeed = 1.5f;

    private float _rotY;
    private Vector3 _offset;

    void Start()
    {
       _rotY = transform.eulerAngles.y;
       _offset = target.position - transform.position;//—охранение начального смещени€ между камерой и целью.
    }

    void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");

         if(horInput !=0)//ћедленный поворот камеры при помощи клавиш со стрелками
        {
            _rotY += horInput * rotSpeed;
        }
        else
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;//или быстрый поворот с помощью мыши
        }

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * _offset);//ѕоддерживаем начальное смещение, сдвигаемое в соответствии с поворотом камеры.
        transform.LookAt(target);// амера всегда направлена на цель, где бы относительно этой цели она ни располагалась
    }
}
