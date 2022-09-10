using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BEAN
{
    public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public TextMeshProUGUI amountText;
        public Image itemSprite;
        public ItemObject item;


        // Detects when player clicks on the slot
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                PopupManager.SetInventorySlotMenu(eventData.pointerCurrentRaycast.gameObject, item, int.Parse(amountText.text));
            } else if (eventData.button == PointerEventData.InputButton.Right)
            {
                // Fast equip item
                DisplayInventory.EquipItemAction(eventData.pointerCurrentRaycast.gameObject, item);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }

    public static class DoubleClickTimer
    {
        
    }
}