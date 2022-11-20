using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xpedition
{
    public class Item : MonoBehaviour
    {
        [Header("Item")]
        public ItemObject item;
        public int amount;
        private bool canTake = false;


        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = item.sprite;
        }

        private void Update()
        {
            List<ItemObject> itemStack = GameManager.selectedItem;
            if (Input.GetKeyDown(KeyCode.E) && canTake && itemStack[itemStack.Count - 1] == item)
            {
                InventoryManager.PickupItemAction(item, amount, this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                canTake = true;
                GameManager.selectedItem.Add(item);
                PopupManager.CreatePopupKeyAction($"{item.itemName}", "E");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            List<ItemObject> itemStack = GameManager.selectedItem;
            if (other.tag == "Player")
            {
                canTake = false;
                GameManager.selectedItem.Remove(item);
                PopupManager.RemovePopupAction();

                // Delete selected item if this is the selected item
                if (itemStack.Count > 0 && itemStack[itemStack.Count - 1] == item) GameManager.selectedItem = null;
            }
        }
    }
}
