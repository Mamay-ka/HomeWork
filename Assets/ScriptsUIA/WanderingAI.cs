using System.Collections;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;
        
    public float speed = 3.0f;

    public float obstacleRange = 5.0f;//расстояние, с которого начинается реакция на препятствие

    private bool _alive;//переменная для слежения за состоянием персонажа

    private const float baseSpeed = 3.0f;//Базовая скорость, меняемая в соответствии с положением ползунка

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void Start()
    {
        _alive = true;
    }

    void Update()
    {
        if (_alive)//Движение начинается только в случае живого персонажа.
        {
            transform.Translate(0, 0, speed * Time.deltaTime);//непрерывно движемся вперед в каждом кадре, несмотря на повороты

            Ray ray = new Ray(transform.position, transform.forward);//луч находится в том же положение и нацеливается в том же положении, что и персонаж
            RaycastHit hit;

            if (Physics.SphereCast(ray, 0.75f, out hit))//бросаем луч с описанной вокруг него окружностью
            {
                GameObject hitObject = hit.transform.gameObject;
                if(hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);//Поместим огненный шар перед врагом и нацелим его в направлении движения
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    transform.Rotate(0, Random.Range(-110, 100), 0);//поворот с наполовину случайным выбором направления
                }
            }
        }   
    }

    public void SetAlive(bool alive)
    { 
        _alive = alive;
    }

    private void OnSpeedChanged(float value)//Метод, объявленный в подписчике для события SPEED_CHANGED
    {
        speed = baseSpeed * value;
        
    }
}
