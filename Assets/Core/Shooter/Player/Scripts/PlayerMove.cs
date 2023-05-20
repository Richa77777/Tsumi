using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");

            _rigidbody.velocity = new Vector3(horizontalAxis * _speed, 0f, 0f);
        }
    }
}
