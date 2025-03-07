using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Player.Systems;

namespace BulletSpace
{
    public class BulletsDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Bullet>())
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
