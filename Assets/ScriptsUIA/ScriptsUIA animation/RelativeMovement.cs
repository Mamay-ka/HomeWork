using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;//ссылка на объект, относительно которого будет происходить перемещение

    public float rotSpeed = 15.0f;

    void Update()
    {
        Vector3 movement = Vector3.zero;//Ќачинаем с вектора (0, 0, 0), непрерывно добавл€€ компоненты движени€

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        if(horInput != 0 || vertInput != 0)//ƒвижение обрабатываетс€ только при нажатии клавиш со стрелками.
        {
            movement.x = horInput;
            movement.z = vertInput;

            Quaternion tmp = target.rotation;//—охран€ем начальную ориентацию, чтобы вернутьс€ к ней после завершени€ работы с целевым объектом
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);//ѕреобразуем направление движени€ из локальных в глобальные координаты
            target.rotation = tmp;

            //transform.rotation = Quaternion.LookRotation(movement);//ћетод LookRotation() вычисл€ет кватернион, смотр€щий в этом направлении
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
    }
}
