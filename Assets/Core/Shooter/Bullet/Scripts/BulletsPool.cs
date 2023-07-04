using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletSpace
{
    public class BulletsPool : MonoBehaviour
    {
        public static BulletsPool Instance;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private int _poolLength;

        private List<Bullet> _bulletsPool = new List<Bullet>();
        public List<Bullet> BulletsPoolGet => new List<Bullet>(_bulletsPool);

        private void Awake()
        {
            AddBulletsInPool(_bulletPrefab, _poolLength);
        }

        private void AddBulletsInPool(GameObject prefab, int count)
        {
            Bullet bullet;

            for (int i = 0; i < count; i++)
            {
                bullet = Instantiate(prefab, transform, true).GetComponent<Bullet>();
                bullet.gameObject.SetActive(false);

                _bulletsPool.Add(bullet);
            }
        }
    }
}
