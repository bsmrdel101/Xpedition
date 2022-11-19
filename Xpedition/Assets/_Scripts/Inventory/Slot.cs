using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Xpedition
{
    public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Slot Data")]
        public Image itemSprite;
        public ItemObject item;
        public int amount;

        [Header("References")]
        [SerializeField] private TextMeshProUGUI amountText;


        private void Start()
        {
            amountText.text = amount.ToString();
        }

        // Detects when player clicks on the slot
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                InventoryManager.DropItemAction(item, amount);
                Destroy(this.gameObject);
            }
            if (eventData.clickCount == 2 && CheckAllowedEquipables())
            {
                InventoryManager.EquipItemAction(item);
                Destroy(this.gameObject);
            }
        }

        // Checks to see if item is an equipable
        // Returns boolean value
        private bool CheckAllowedEquipables()
        {
            if (item.type == ItemType.Tool || item.type == ItemType.Weapon)
            {
                return true;
            }
            return false;
        }

        // TODO: Show tooltip on hover

        public void OnPointerEnter(PointerEventData eventData)
        {
            // PopupManager.SetInventorySlotMenu(eventData.pointerCurrentRaycast.gameObject, item, int.Parse(amountText.text));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // PopupManager.RemoveInventorySlotMenuAction();
        }
    }
}
