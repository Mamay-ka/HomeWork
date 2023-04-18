using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public delegate void JumpingEvent();
    public event JumpingEvent playerJump;

    private GameBehaviour _gameManager;

    public float _moveSpeed = 10f;
    public float _rotateSpeed = 75f;
    public float _jumpVelocity = 5f;
    public float _distanceToGround = 0.01f;
    public LayerMask _groundLayer;
    public GameObject _bullet;
    public float _bulletSpeed = 100f;

    private float _vInput;
    private float _hInput;
    private CapsuleCollider _col;
    

    private Rigidbody _rb;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>();

        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * _moveSpeed;
        _hInput = Input.GetAxis("Horizontal") * _rotateSpeed;


        //this.transform.Translate(Vector3.forward * _vInput * Time.deltaTime);
        //this.transform.Rotate(Vector3.up * _hInput * Time.deltaTime);

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _jumpVelocity, ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(_bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation)
                as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * _bulletSpeed;
        }
    }

   
    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(angleRot * _rb.rotation);

        if(!IsGrounded())
        {
            playerJump();
        }
        
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, _distanceToGround, _groundLayer,
                QueryTriggerInteraction.Ignore);
        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.PlayerHP -= 1;
        }
    }
}
