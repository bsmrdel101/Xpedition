using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xpedition
{
    [CreateAssetMenu(fileName = "Food", menuName = "Inventory System/Items/New Food")]
    public class FoodObject : ItemObject
    {
        public int healValue;
        void Awake()
        {
            type = ItemType.Food;
        }
    }
}