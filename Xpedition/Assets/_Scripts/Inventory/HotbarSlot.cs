using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Xpedition
{
    public class HotbarSlot : MonoBehaviour, IPointerClickHandler
    {
        [Header("Hotbar")]
        public Image hotbarImage;
        public GameObject itemSprite;
        public ItemObject item;


        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                InventoryManager.DropItemAction(item, 1);
                itemSprite.SetActive(false);
            }
            if (eventData.clickCount == 2)
            {
                InventoryManager.DequipItemAction(item);
                itemSprite.SetActive(false);
            }
        }
    }
}
