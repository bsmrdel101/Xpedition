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
            if (Input.GetKeyDown(KeyCode.E) && canTake && GameManager.selectedItem == item)
            {
                InventoryManager.PickupItemAction(item, amount, this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                canTake = true;
                GameManager.selectedItem = item;
                PopupManager.CreatePopupKeyAction($"{item.itemName}", "E");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                canTake = false;
                PopupManager.RemovePopupAction();

                // Delete selected item if this is the selected item
                if (GameManager.selectedItem == item) GameManager.selectedItem = null;
            }
        }
    }
}
