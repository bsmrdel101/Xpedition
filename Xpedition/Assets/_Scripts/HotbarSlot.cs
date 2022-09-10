using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BEAN
{
    public enum SlotType
    {
        Weapon,
        Tool,
    }

    public class HotbarSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public SlotType type;
        public Image image;
        public ItemObject item;


        // Detects when player clicks on the slot
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // PopupManager.SetInventorySlotMenu(eventData.pointerCurrentRaycast.gameObject, item, 1);
            } else if (eventData.button == PointerEventData.InputButton.Right)
            {
                // Fast equip item
                DisplayInventory.DequipItemAction(eventData.pointerCurrentRaycast.gameObject, item);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
}