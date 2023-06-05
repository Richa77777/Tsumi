using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Systems
{
    public class PlayerSystemsInitializer : MonoBehaviour
    {
        [SerializeField] private GameObject _heartsPrefab;
        [SerializeField] private GameObject _reloadingBarPrefab;
        [SerializeField] private GameObject _victoryBarPrefab;

        private HeartsController _heartsController;
        private BarController _reloadingBarController;
        private BarController _victoryBarController;

        public HeartsController HeartsControllerGet => _heartsController;
        public BarController ReloadingBarControllerGet => _reloadingBarController;
        public BarController VictoryBarControllerGet => _victoryBarController;

        private void Start()
        {
            _heartsController = Instantiate(_heartsPrefab).GetComponent<HeartsController>();
            _reloadingBarController = Instantiate(_reloadingBarPrefab).GetComponent<BarController>();
            _victoryBarController = Instantiate(_victoryBarPrefab).GetComponent<BarController>();
        }
    }
}
