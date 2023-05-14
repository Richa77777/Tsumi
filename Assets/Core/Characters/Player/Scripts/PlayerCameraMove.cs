using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMove : MonoBehaviour
{
    [SerializeField] private float _moveOffset = 2f;

    private bool _moveBlocked = false;

    private void Update()
    {
        if (_moveBlocked == false)
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");

            if (horizontalAxis != 0)
            {
                transform.position = new Vector3(transform.position.x + (_moveOffset * Time.deltaTime) * horizontalAxis, transform.position.y, transform.position.z);
            }

            if (verticalAxis != 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + (_moveOffset * Time.deltaTime) * verticalAxis, transform.position.z);
            }

        }
    }

    public void BlockMove()
    {
        _moveBlocked = true;
    }

    public void UnblockMove()
    {
        _moveBlocked = false;
    }
}
