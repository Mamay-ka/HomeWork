using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;//Сериализованная переменная для связи с объектом-шаблоном.

    private GameObject _enemy;//Закрытая переменная для слежения за экземпляром врага в сцене

    void Update()
    {
       if(_enemy == null)//Порождаем нового врага, только если враги в сцене отсутствуют
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;//Метод, копирующий объект-шаблон
            _enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
        }
        
    }
}
