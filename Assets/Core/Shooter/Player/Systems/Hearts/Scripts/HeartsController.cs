using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Systems
{
    public class HeartsController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _hearts = new GameObject[3];
    }
}
