using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xpedition
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Inventory System/Items/New Weapon")]
    public class WeaponObject : ItemObject
    {
        public float damage;
        public float atkSpeed;
        public float atkRange;

        void Awake()
        {
            type = ItemType.Weapon;
        }
    }
}