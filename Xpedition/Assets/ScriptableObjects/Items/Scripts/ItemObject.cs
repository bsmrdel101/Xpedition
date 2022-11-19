using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xpedition
{
    public enum ItemType
    {
        Default,
        Material,
        Weapon,
        Food,
        Tool,
    }

    public enum ItemTier
    {
        None,
        Tier1,
        Tier2,
        Tier3,
    }

    public abstract class ItemObject : ScriptableObject
    {
        public string itemName;
        public GameObject prefab;
        public ItemType type;
        public ItemTier itemTier;
        public Sprite sprite;

        [TextArea(15,20)]
        public string description;
    }
}