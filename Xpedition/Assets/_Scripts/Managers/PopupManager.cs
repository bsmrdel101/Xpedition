using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BEAN
{
    public class PopupManager : MonoBehaviour
    {
        [Header("Popups")]
        [SerializeField] private TextMeshProUGUI popupText;
        [SerializeField] private GameObject popupInventorySlotMenu;
        [SerializeField] private RectTransform inventoryPos;

        [Header("Actions")]
        // Normal popup
        public static Action<string> CreatePopupAction;
        public static Action RemovePopupAction;
        // Inventory slot menu popup
        public static Action<GameObject, ItemObject, int> SetInventorySlotMenu;

        [Header("References")]
        [SerializeField] private GameObject equipBtn;
        [SerializeField] private GameObject dequipBtn;
        private GameObject selectedSlot;
        private ItemObject selectedItem;
        private int selectedItemAmount;


        // Subscribes the functions to their respective actions
        private void OnEnable()
        {
            CreatePopupAction += CreatePopup;
            RemovePopupAction += RemovePopup;
            SetInventorySlotMenu += SetInventorySlotPopup;
        }

        // Unsubscribes the functions from their respective actions
        private void OnDisable()
        {
            CreatePopupAction -= CreatePopup;
            RemovePopupAction -= RemovePopup;
            SetInventorySlotMenu -= SetInventorySlotPopup;
        }

        // Create and display a popup on screen
        private void CreatePopup(string text)
        {
            popupText.text = text;
        }

        private void RemovePopup()
        {
            popupText.text = default;
        }

        // Set a popup that lets the player equip or drop item
        private void SetInventorySlotPopup(GameObject slot, ItemObject item, int amount)
        {
            popupInventorySlotMenu.SetActive(true);
            popupInventorySlotMenu.transform.SetParent(inventoryPos);
            popupInventorySlotMenu.transform.position = new Vector2(Input.mousePosition.x + 130, Input.mousePosition.y + 120);
            selectedSlot = slot;
            selectedItem = item;
            selectedItemAmount = amount;
        }

        // Runs when player clicks the drop item button
        public void OnClickDropItem()
        {
            DisplayInventory.DropItemAction(selectedSlot, selectedItem, selectedItemAmount);
        }

        // Runs when player clicks the equip item button
        public void OnClickEquipItem()
        {
            DisplayInventory.EquipItemAction(selectedSlot, selectedItem);
        }

        // Runs when player clicks the dequip item button
        public void OnClickDequipItem()
        {
            DisplayInventory.DequipItemAction(selectedSlot, selectedItem);
        }
    }
}