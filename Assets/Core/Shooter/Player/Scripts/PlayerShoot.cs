using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bullet;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletsPoolPrefab;
        [SerializeField] private GameObject _reloadingBarPrefab;

        private BulletsPool _bulletsPool;
        private ReloadingBar _reloadingBar;

        private void Start()
        {
            _bulletsPool = Instantiate(_bulletsPoolPrefab).GetComponent<BulletsPool>();
            _reloadingBar = Instantiate(_reloadingBarPrefab).GetComponentInChildren<ReloadingBar>();
        }
    }
}
