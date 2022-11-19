using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xpedition
{
    [CreateAssetMenu(fileName = "Default Item", menuName = "Inventory System/Items/Default")]
    public class DefaultObject : ItemObject
    {
        void Awake()
        {
            type = ItemType.Default;
        }
    }
}