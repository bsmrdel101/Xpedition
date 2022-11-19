using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Xpedition
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float health, maxHealth = 20f;
        [SerializeField] private Slider healthBar;

        [Header("References")]
        [SerializeField] private GameObject pauseMenu;


        private void Start()
        {
            health = maxHealth;
        }

        private void Update()
        {
            // Pause game
            if (Input.GetKeyDown(KeyCode.Escape)) TogglePauseMenu();

            // Quicksave
            if (Input.GetKeyDown(KeyCode.F5))
            {
                
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

        private void TogglePauseMenu()
        {
            GameManager.paused = !GameManager.paused;
            pauseMenu.SetActive(GameManager.paused);
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
