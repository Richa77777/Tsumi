using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Systems
{
    public class PlayerVictoryBarController : MonoBehaviour
    {
        [SerializeField] private float _barFillDuration;

        private PlayerSystemsInitializer _playerSystems;

        private void Start()
        {
            _playerSystems = GetComponent<PlayerSystemsInitializer>();
        }

        public void SetBarValue(float value)
        {
            _playerSystems.VictoryBarControllerGet.SetBarValue(value, _barFillDuration);
        }
    }
}