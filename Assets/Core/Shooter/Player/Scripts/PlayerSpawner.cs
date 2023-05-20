using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;

        private void Start()
        {
            Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        }
    }
}