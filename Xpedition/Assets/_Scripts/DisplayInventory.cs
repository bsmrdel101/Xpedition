using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace BEAN
{
    public class DisplayInventory : MonoBehaviour
    {
        [Header("Actions")]
        public static Action UpdateInventoryAction;
        public static Action<GameObject, ItemObject, int> DropItemAction;
        public static Action<GameObject, ItemObject> EquipItemAction;
        public static Action<GameObject, ItemObject> DequipItemAction;

        [Header("Inventory")]
        public InventoryObject inventory;
        [SerializeField] private GameObject inventoryUI;
        [SerializeField] private Transform storageUI;

        [Header("Hotbar")]
        [SerializeField] private HotbarSlot weaponSlot;
        [SerializeField] private HotbarSlot toolSlot;

        [Header("References")]
        [SerializeField] private Transform playerPos;
        [SerializeField] private GameObject groundItemPrefab;

        private List<GameObject> itemsDisplayed = new List<GameObject>();


        void Start()
        {
            CreateDisplay();
        }

        private void OnEnable()
        {
            UpdateInventoryAction += UpdateInventory;
            DropItemAction += DropItem;
            EquipItemAction += EquipItem;
            DequipItemAction += DequipItem;
        }

        private void OnDisable()
        {
            UpdateInventoryAction -= UpdateInventory;
            DropItemAction -= DropItem;
            EquipItemAction -= EquipItem;
            DequipItemAction -= DequipItem;
        }

        private void CreateDisplay()
        {
            inventoryUI.SetActive(true);
            inventory.Load();
            // Render inventory
            foreach (InventorySlot slot in inventory.Container)
            {
                var obj = Instantiate(slot.item.prefab, Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(storageUI);
                obj.GetComponent<Slot>().amountText.text = slot.amount.ToString("n0");
                obj.GetComponent<Slot>().itemSprite.sprite = slot.item.sprite;
                obj.GetComponent<Slot>().item = slot.item;
                itemsDisplayed.Add(obj);
            }

            // Render hotbar
            int i = 0;
            foreach (InventorySlot slot in inventory.Hotbar)
            {
                if (i == 0)
                {
                    // Render in weapon slot
                    HotbarSlot hotbarSlot = weaponSlot.GetComponent<HotbarSlot>();
                    hotbarSlot.item = slot.item;
                    hotbarSlot.image.sprite = slot.item.sprite;
                    hotbarSlot.image.gameObject.SetActive(true);
                } else
                {
                    // Render in tool slot
                    HotbarSlot hotbarSlot = weaponSlot.GetComponent<HotbarSlot>();
                    hotbarSlot.item = slot.item;
                    hotbarSlot.image.sprite = slot.item.sprite;
                    hotbarSlot.image.gameObject.SetActive(true);
                }
                i++;
            }

            inventoryUI.SetActive(false);
        }

        public void UpdateInventory()
        {
            // Destory all currently rendered inventory items
            for (int i = 0; i < itemsDisplayed.Count; i++)
                Destroy(itemsDisplayed[i]);

            // Clear itemsDisplayed list
            itemsDisplayed.Clear();

            // Render inventory
            foreach (InventorySlot slot in inventory.Container)
            {
                var obj = Instantiate(slot.item.prefab, Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(storageUI);
                obj.GetComponent<Slot>().amountText.text = slot.amount.ToString("n0");
                obj.GetComponent<Slot>().itemSprite.sprite = slot.item.sprite;
                obj.GetComponent<Slot>().item = slot.item;
                itemsDisplayed.Add(obj);
            }

            // If hotbar is not updating display then you might need to render hotbar here 
        }

        public void DropItem(GameObject selectedSlot, ItemObject selectedItem, int selectedItemAmount)
        {
            // Spawn ground object
            GameObject droppedItem = Instantiate(groundItemPrefab, playerPos.position, Quaternion.identity);
            // Set values
            Item itemScript = droppedItem.GetComponent<Item>();
            itemScript.item = selectedItem;
            itemScript.amount = selectedItemAmount;
            itemScript.canTake = false;
            itemScript.GetComponent<SpriteRenderer>().sprite = selectedItem.sprite;

            // Remove slot from inventory
            foreach (InventorySlot slot in inventory.Container)
            {
                if (slot.item == selectedItem)
                {
                    inventory.Container.Remove(slot);
                    break;
                }
            }
            Destroy(selectedSlot);
        }

        public void EquipItem(GameObject selectedSlot, ItemObject selectedItem)
        {
            if (selectedItem.type == ItemType.MeleeWeapon || selectedItem.type == ItemType.RangedWeapon)
            {
                // Equip to weapon slot
                HotbarSlot hotbarSlot = weaponSlot.GetComponent<HotbarSlot>();
                hotbarSlot.item = selectedItem;
                hotbarSlot.image.sprite = selectedItem.sprite;
                hotbarSlot.image.gameObject.SetActive(true);

                // Remove slot from inventory
                foreach (InventorySlot slot in inventory.Container)
                {
                    if (slot.item == selectedItem)
                    {
                        inventory.Container.Remove(slot);
                        inventory.Hotbar.Add(slot);
                        break;
                    }
                }
                Destroy(selectedSlot);
            } else if (selectedItem.type == ItemType.Tool)
            {
                // Equip to tool slot
                HotbarSlot hotbarSlot = toolSlot.GetComponent<HotbarSlot>();
                hotbarSlot.item = selectedItem;
                hotbarSlot.image.sprite = selectedItem.sprite;
                hotbarSlot.image.gameObject.SetActive(true);

                // Remove slot from inventory
                foreach (InventorySlot slot in inventory.Container)
                {
                    if (slot.item == selectedItem)
                    {
                        inventory.Container.Remove(slot);
                        inventory.Hotbar.Add(slot);
                        break;
                    }
                }
                Destroy(selectedSlot);
            }
        }

        public void DequipItem(GameObject selectedSlot, ItemObject selectedItem)
        {
            HotbarSlot hotbarSlot = selectedSlot.GetComponent<HotbarSlot>();
            InventoryObject.AddItemAction(selectedItem, 1);
            hotbarSlot.item = null;
            hotbarSlot.image.sprite = null;
            hotbarSlot.image.gameObject.SetActive(false);

            // Remove item from hotbar
            foreach (InventorySlot slot in inventory.Hotbar)
            {
                if (slot.item == selectedItem)
                {
                    inventory.Hotbar.Remove(slot);
                    break;
                }
            }
        }
    }
}