using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletSpace;
using Player.Systems;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private GameObject _shootPoint;
        [SerializeField] private GameObject _bulletsPoolPrefab;

        [SerializeField] private float _reloadDuration = 0.75f;

        private PlayerSystemsInitializer _playerSystems;
        private BulletsPool _bulletsPoolComponent;

        private bool _canShoot = true;

        public BulletsPool BulletsPoolComponentGet => _bulletsPoolComponent;

        private void Start()
        {
            _playerSystems = GetComponent<PlayerSystemsInitializer>();
            _bulletsPoolComponent = Instantiate(_bulletsPoolPrefab).GetComponent<BulletsPool>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if (_canShoot == true)
            {
                Bullet bullet = null;

                for (int i = 0; i < _bulletsPoolComponent.BulletsPoolGet.Count; i++)
                {
                    if (_bulletsPoolComponent.BulletsPoolGet[i].gameObject.activeInHierarchy == false)
                    {
                        bullet = _bulletsPoolComponent.BulletsPoolGet[i];
                        break;
                    }
                }

                bullet.transform.position = _shootPoint.transform.position;
                bullet.gameObject.SetActive(true);

                StartCoroutine(Reloading());
            }
        }

        private IEnumerator Reloading()
        {
            _canShoot = false;

            _playerSystems.ReloadingBarControllerGet.SetBarValue(0, 0);
            _playerSystems.ReloadingBarControllerGet.SetBarValue(1, _reloadDuration);

            yield return new WaitForSeconds(_reloadDuration);

            _canShoot = true;
        }
    }
}
