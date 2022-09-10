using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BEAN
{
    [CreateAssetMenu(fileName = "Tool", menuName = "Inventory System/Items/New Tool")]
    public class ToolObject : ItemObject
    {
        public float mineSpeed;

        void Awake()
        {
            type = ItemType.Tool;
        }
    }
}