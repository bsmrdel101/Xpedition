using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Xpedition
{
    public class PopupManager : MonoBehaviour
    {
        [Header("Text Popup")]
        [SerializeField] private TextMeshProUGUI popupText;

        [Header("Key Popup")]
        [SerializeField] private GameObject popupKey;
        [SerializeField] private TextMeshProUGUI popupKeyText;

        [Header("Inventory")]
        [SerializeField] private GameObject popupInventorySlotMenu;
        [SerializeField] private RectTransform inventoryPos;

        [Header("Actions")]
        public static Action<string> CreatePopupAction;
        public static Action<string, string> CreatePopupKeyAction;
        public static Action RemovePopupAction;
        public static Action<GameObject, ItemObject, int> SetInventorySlotMenu;
        public static Action RemoveInventorySlotMenuAction;

        [Header("References")]
        [SerializeField] private GameObject equipBtn;
        [SerializeField] private GameObject dequipBtn;
        private GameObject selectedSlot;
        private ItemObject selectedItem;
        private int selectedItemAmount;


        private void OnEnable()
        {
            CreatePopupAction += CreatePopup;
            CreatePopupKeyAction += CreatePopupKey;
            RemovePopupAction += RemovePopup;
            SetInventorySlotMenu += SetInventorySlotPopup;
            RemoveInventorySlotMenuAction += RemoveInventorySlotPopup;
        }

        private void OnDisable()
        {
            CreatePopupAction -= CreatePopup;
            CreatePopupKeyAction -= CreatePopupKey;
            RemovePopupAction -= RemovePopup;
            SetInventorySlotMenu -= SetInventorySlotPopup;
            RemoveInventorySlotMenuAction -= RemoveInventorySlotPopup;
        }

        // Create and display a popup on screen
        private void CreatePopup(string text)
        {
            popupText.text = text;
        }

        // Create and display a popup on screen, with a key to press
        private void CreatePopupKey(string text, string key)
        {
            popupKey.SetActive(true);
            popupText.text = text;
            popupKeyText.text = key;
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

        private void RemoveInventorySlotPopup()
        {
            popupInventorySlotMenu.SetActive(false);
        }

        private void RemovePopup()
        {
            popupText.text = default;
            popupKey.SetActive(false);
        }
    }
}