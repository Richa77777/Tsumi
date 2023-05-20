using UnityEngine;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _rigidbody.velocity = new Vector3(0f, _speed, 0f);
        }
    }
}