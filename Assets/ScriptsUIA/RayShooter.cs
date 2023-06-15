using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
   
    void Start()
    {
        _camera = GetComponent<Camera>();
        //Cursor.lockState = CursorLockMode.Locked;//скрываем указатель мыши в центре экрана
        //Cursor.visible = false;//делаем его невидимым
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth/2 - size/4;
        float posY = _camera.pixelHeight/ 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");//Команда GUI.Label() отображает на экране символ
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//Проверяем, что GUI не используется.
        {
            Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);//середина экрана 
            Ray ray = _camera.ScreenPointToRay(point);//луч выпущенный из середины экрана
            RaycastHit hit;//переменная для объектов куда попадает луч
            if(Physics.Raycast(ray, out hit))//создание луча и получение инфы о попадании
            {
                GameObject hitObject = hit.transform.gameObject;//получаем объект в который попал луч
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if(target != null)//проверяем наличие у этого компонента  ReactiveTarget
                {
                    Debug.Log("Target hit");
                    target.ReactToHit();
                    Messenger.Broadcast(GameEvent.ENEMY_HIT);//К реакции на попадания добавлена рассылка сообщения.
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
         }
            
    }
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(2);
        Destroy(sphere);
    }

}
