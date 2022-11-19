using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xpedition
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private int damage = 3;

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                player.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }
    }
}
