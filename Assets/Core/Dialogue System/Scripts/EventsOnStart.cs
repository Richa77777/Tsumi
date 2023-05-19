using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsOnStart : MonoBehaviour
{
    [SerializeField] private UnityEvent _startEvents;

    private void Start()
    {
        _startEvents?.Invoke();
    }
}
