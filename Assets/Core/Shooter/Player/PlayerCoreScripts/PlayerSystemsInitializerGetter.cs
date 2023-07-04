using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Systems
{
    public class PlayerSystemsInitializerGetter : MonoBehaviour
    {
        public static PlayerSystemsInitializerGetter Instance;

        private PlayerSystemsInitializer _playerSystemsInitializer;
        public PlayerSystemsInitializer PlayerSystemsInitializerGet => _playerSystemsInitializer;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void InitializeInitializer(PlayerSystemsInitializer initializer)
        {
            _playerSystemsInitializer = initializer;
        }
    }
}
