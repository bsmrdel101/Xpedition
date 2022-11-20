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

        [Header("Dragging")]
        GameObject ghostImageObj;
        private GraphicRaycaster m_Raycaster;
        private PointerEventData m_PointerEventData;
        private EventSystem m_EventSystem;

        [Header("References")]
        public TextMeshProUGUI amountText;
        [SerializeField] private Image ghostImage;
        private GameObject inventoryCanvas;


        private void Start()
        {
            amountText.text = amount.ToString();
            inventoryCanvas = GameObject.Find("InventoryCanvas");
            m_Raycaster = inventoryCanvas.GetComponent<GraphicRaycaster>();
            m_EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }

        // Detects when player clicks on the slot
        public void OnPointerClick(PointerEventData eventData)
        {
            // Right click
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                InventoryManager.DropItemAction(item, amount);
                Destroy(this.gameObject);
            }

            // Double click
            if (eventData.clickCount == 2 && CheckAllowedEquipables())
            {
                InventoryManager.EquipItemAction(item, amount);
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

        public void OnDragStart(BaseEventData eventData)
        {
            PointerEventData pointerData = eventData as PointerEventData;
            if (pointerData.button != PointerEventData.InputButton.Left) return;
            ghostImage.sprite = item.sprite;
            ghostImageObj = Instantiate(ghostImage.gameObject, Vector3.zero, Quaternion.identity);
            ghostImageObj.transform.SetParent(GameObject.Find("InventoryCanvas").transform);
        }

        public void OnDrag()
        {
            if (ghostImageObj)
                ghostImageObj.transform.position = Input.mousePosition;
        }

        public void OnDragEnd()
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);

            // Handle stop dragging item
            foreach (RaycastResult result in results)
            {
                switch (result.gameObject.name)
                {
                    case "WeaponHotbarSlot":
                        // Equip weapon
                        if (!result.gameObject.GetComponent<HotbarSlot>().item && CheckAllowedEquipables())
                        {
                            InventoryManager.EquipItemAction(item, amount);
                            Destroy(this.gameObject);
                        }
                        break;
                    case "ToolHotbarSlot":
                        // Equip tool
                        if (!result.gameObject.GetComponent<HotbarSlot>().item && CheckAllowedEquipables())
                        {
                            InventoryManager.EquipItemAction(item, amount);
                            Destroy(this.gameObject);
                        }
                        break;
                    default:
                        break;
                }
            }
            Destroy(ghostImageObj);
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
