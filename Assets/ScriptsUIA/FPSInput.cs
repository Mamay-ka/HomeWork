using UnityEngine;

namespace UIA
{
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("Control script/FPSInput")]

    public class FPSInput : MonoBehaviour
    {
        public float speed = 6f;
        public float gravity = -9.8f;

        private const float baseSpeed = 6.0f;
        private CharacterController _charController;

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
            _charController = GetComponent<CharacterController>();
        }

        void Update()
        {
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            
            movement = Vector3.ClampMagnitude(movement, speed);//��������� �������� �� ��������� ����� �� ���������, ��� � �����.
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);//����������� ������ �������� �� ��������� � ���������� �����������
            _charController.Move(movement);//�������� ���� ������ ���������� ��������� ����o��������
        }

        private void OnSpeedChanged(float value)
        {
            speed = baseSpeed * value;
        }
        
    }
}
