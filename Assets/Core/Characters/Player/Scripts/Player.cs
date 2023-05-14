using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        private PlayerCameraMove _playerCameraMove;
        public PlayerCameraMove PlayerCameraMoveGet => _playerCameraMove;

        private void Awake()
        {
            _playerCameraMove = GetComponent<PlayerCameraMove>();

            #region Singleton
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            else
            {
                Destroy(gameObject);
            }
            #endregion
        }

    }
}
