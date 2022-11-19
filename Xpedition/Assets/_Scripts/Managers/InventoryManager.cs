using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xpedition
{
    public class InventoryManager : MonoBehaviour
    {
        [Header("Actions")]
        public static Action<ItemObject, int, GameObject> PickupItemAction;
        public static Action<ItemObject, int> DropItemAction;
        public static Action<ItemObject> EquipItemAction;
        public static Action<ItemObject> DequipItemAction;

        [Header("Inventory")]
        [SerializeField] private GameObject inventoryUI;
        [SerializeField] private Transform inventoryStorage;

        [Header("Hotbar")]
        [SerializeField] private HotbarSlot weaponSlot;
        [SerializeField] private HotbarSlot toolSlot;

        [Header("References")]
        [SerializeField] private Transform playerPos;
        [SerializeField] private GameObject groundItemPrefab;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab)) ToggleInventory();
        }

        private void OnEnable()
        {
            PickupItemAction += PickupItem;
            DropItemAction += DropItem;
            EquipItemAction += EquipItem;
            DequipItemAction += DequipItem;
        }

        private void OnDisable()
        {
            PickupItemAction -= PickupItem;
            DropItemAction -= DropItem;
            EquipItemAction -= EquipItem;
            DequipItemAction -= DequipItem;
        }

        private void ToggleInventory()
        {
            GameManager.inventoryOpened = !GameManager.inventoryOpened;
            inventoryUI.SetActive(GameManager.inventoryOpened);
        }

        private void PickupItem(ItemObject item, int amount, GameObject obj)
        {
            if (obj) Destroy(obj); // Remove ground object if it exists
            AddItemToInventory(item, amount);
        }

        private void AddItemToInventory(ItemObject item, int amount)
        {
            GameObject slot = Instantiate(item.prefab, Vector3.zero, Quaternion.identity);
            Slot slotData = slot.GetComponent<Slot>();
            slot.transform.SetParent(inventoryStorage);
            slotData.item = item;
            slotData.itemSprite.sprite = item.sprite;
            slotData.amount = amount;

            // Set scale to 1 or it doesn't show up
            slot.transform.localScale = new Vector3(1, 1, 1);
        }

        private void DropItem(ItemObject item, int amount)
        {
            // Spawn ground object
            GameObject droppedItem = Instantiate(groundItemPrefab, playerPos.position, Quaternion.identity);
            
            // Set values
            Item itemData = droppedItem.GetComponent<Item>();
            itemData.item = item;
            itemData.amount = amount;
            itemData.GetComponent<SpriteRenderer>().sprite = item.sprite;
        }

        private void EquipItem(ItemObject item)
        {
            switch (item.type)
            {
                case ItemType.Weapon:
                    HandleItemEquipProcess(weaponSlot, item);
                    break;
                case ItemType.Tool:
                    HandleItemEquipProcess(toolSlot, item);
                    break;
                default:
                    break;
            }
        }

        // Updates hotbar image and data
        private void HandleItemEquipProcess(HotbarSlot slot, ItemObject item)
        {
            slot.item = item;
            slot.hotbarImage.sprite = item.sprite;
            slot.itemSprite.SetActive(true);
        }

        private void DequipItem(ItemObject item)
        {
            AddItemToInventory(item, 1);
        }
    }
}
