using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BEAN
{
    public enum ItemType
    {
        Default,
        Material,
        MeleeWeapon,
        RangedWeapon,
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
        public GameObject prefab;
        public ItemType type;
        public ItemTier itemTier;
        public Sprite sprite;

        [TextArea(15,20)]
        public string description;
    }
}