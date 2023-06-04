using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public class BulletsPool : MonoBehaviour
    {
        public static BulletsPool Instance;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private int _poolLength;

        private List<Bullet> _bulletsPool;
        public List<Bullet> BulletsPoolGet => _bulletsPool;

        private void Awake()
        {
            AddBulletsInPool(_bulletPrefab, _poolLength);
        }

        private void AddBulletsInPool(GameObject prefab, int count)
        {
            GameObject bullet;

            for (int i = 0; i < count; i++)
            {
                bullet = Instantiate(prefab, transform, true);
                bullet.SetActive(false);
            }
        }
    }
}
