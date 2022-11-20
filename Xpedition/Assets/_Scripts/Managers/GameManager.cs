using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xpedition
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game State")]
        public static bool paused = false;
        public static bool inventoryOpened = false;

        [Header("Global Data")]
        // TODO: Make this a stack of items
        public static List<ItemObject> selectedItem = new List<ItemObject>();
    }
}
