using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private GameObject _body;

        private Rigidbody2D _rigidbody;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal") * _speed, 0f, 0f);
        }

        private void FixedUpdate()
        {
            Move();
            Flip();
        }

        private void Move()
        {
            _rigidbody.velocity = _moveDirection;
        }

        private void Flip()
        {
            if (_moveDirection.x > 0)
            {
                // To right
                _body.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            else if (_moveDirection.x < 0)
            {
                // To left
                _body.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
