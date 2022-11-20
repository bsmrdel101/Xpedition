using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Xpedition
{
    public class HotbarSlot : MonoBehaviour, IPointerClickHandler
    {
        [Header("Hotbar")]
        public Image hotbarImage;
        public GameObject itemSprite;
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
            inventoryCanvas = GameObject.Find("InventoryCanvas");
            m_Raycaster = inventoryCanvas.GetComponent<GraphicRaycaster>();
            m_EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount == 2 || eventData.button == PointerEventData.InputButton.Right)
            {
                if (!item) return;

                InventoryManager.DequipItemAction(item);
                amountText = default;
                itemSprite.SetActive(false);
                item = null;
            }
        }

        public void OnDragStart()
        {
            ghostImage.sprite = item.sprite;
            ghostImageObj = Instantiate(ghostImage.gameObject, Vector3.zero, Quaternion.identity);
            ghostImageObj.transform.SetParent(GameObject.Find("InventoryCanvas").transform);
        }

        public void OnDrag()
        {
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
                if (result.gameObject.name == "Storage" && item)
                {
                    InventoryManager.DequipItemAction(item);
                    amountText = default;
                    itemSprite.SetActive(false);
                    item = null;
                }
            }
            Destroy(ghostImageObj);
        }
    }
}
