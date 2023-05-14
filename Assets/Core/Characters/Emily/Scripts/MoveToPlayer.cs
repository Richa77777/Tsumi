using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSpace;

namespace EmilySpace
{
    public class MoveToPlayer : MonoBehaviour
    {
        [SerializeField] private float _minDistanceFromPlayer;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private GameObject _body;

        private Player _player;
        private Rigidbody2D _rigidbody;

        private float _distanceFromPlayer = 0f;

        private void Start()
        {
            _player = Player.Instance;
            _rigidbody = GetComponent<Rigidbody2D>();
            _distanceFromPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);
        }

        private void Update()
        {
            _distanceFromPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);

            MovingToPlayer();
        }

        private void MovingToPlayer()
        {
            Vector3 localPos = -_player.transform.InverseTransformPoint(transform.position);
            float direction = localPos.x >= 0 ? 1 : -1;

            Flip(direction);

            if (_distanceFromPlayer > _minDistanceFromPlayer)
            {
                _rigidbody.velocity = new Vector3(direction * _speed, 0f, 0f);
            }

            else if (_distanceFromPlayer <= _minDistanceFromPlayer)
            {
                StopMovingToPlayer();
            }
        }

        private void StopMovingToPlayer()
        {
            if (_rigidbody.velocity != Vector2.zero)
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }

        private void Flip(float direction)
        {
            if (direction > 0)
            {
                // To right
                _body.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            else if (direction < 0)
            {
                // To left
                _body.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
