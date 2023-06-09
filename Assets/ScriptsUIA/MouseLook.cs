using UnityEngine;

namespace UIA
{
    public class MouseLook : MonoBehaviour
    {
        public enum RotationAxes
        {
            MouseXandY = 0,
            MouseX = 1,
            MouseY = 2

        }

        public RotationAxes axes = RotationAxes.MouseXandY;

        public float sensivityHor = 9f;
        public float sensivityVert = 9f;

        public float minimumVert = -45f;
        public float maximumVert = 45f;

        private float _rotationX = 0f;

        private void Update()
        {
            if(axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHor, 0);
            }
            else if (axes == RotationAxes.MouseY)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
                _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
                float rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
            else
            {
                _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
                _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

                float delta = Input.GetAxis("Mouse X") * sensivityHor;
                float rotationY = transform.localEulerAngles.y + delta;
                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
            
        }

        private void Start()
        {
            Rigidbody body = GetComponent<Rigidbody>();
            if(body != null)
            {
                body.freezeRotation = true;
            }
        }
    }
}
