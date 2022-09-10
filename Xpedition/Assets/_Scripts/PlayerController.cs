using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BEAN
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Inventory")]
        [SerializeField] private InventoryObject inventory;
        [SerializeField] private GameObject inventoryUI;

        [Header("Health")]
        [SerializeField] private float health, maxHealth = 20f;
        [SerializeField] private Slider healthBar;

        [Header("References")]
        [SerializeField] private GameObject pauseMenu;


        void Start()
        {
            health = maxHealth;
        }

        void Update()
        {
            // Pause game
            if (Input.GetKeyDown(KeyCode.Escape)) TogglePauseMenu();
            
            // Open inventory
            if (Input.GetKeyDown(KeyCode.Tab)) ToggleInventory();

            // Quicksave
            if (Input.GetKeyDown(KeyCode.F5))
            {
                inventory.Save();
            }

            // Handle game paused
            if (GameManager.paused)
            {
                Cursor.visible = false;
            } else
            {
                Cursor.visible = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var item = other.GetComponent<Item>();
            if (item && item.canTake)
            {
                inventory.AddItem(item.item, item.amount);
                Destroy(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var item = other.GetComponent<Item>();
            if (item) item.canTake = true;
        }

        private void OnApplicationQuit()
        {
            inventory.Container.Clear();
            inventory.Hotbar.Clear();
        }

        private void TogglePauseMenu()
        {
            if (GameManager.inventoryOpened) ToggleInventory();

            GameManager.paused = !GameManager.paused;
            pauseMenu.SetActive(GameManager.paused);
        }

        private void ToggleInventory()
        {
            GameManager.inventoryOpened = !GameManager.inventoryOpened;
            inventoryUI.SetActive(GameManager.inventoryOpened);
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            healthBar.value = health;
            if (health <= 0) 
            {
                Destroy(this.gameObject);
            }
        }
    } 
}