using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Items;

namespace Inventory
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        public int slotIndex;
        public Item slotsItem;

        void Update()
        {
            if(slotsItem != null)
            {
                gameObject.GetComponent<Image>().sprite = slotsItem.itemSprite;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(GetComponentInParent<InventoryManager>().floatingItem != null)
            {
                if(slotsItem == null)
                {
                    slotsItem = GetComponentInParent<InventoryManager>().floatingItem;
                    slotsItem.slotindex = slotIndex;
                    GetComponentInParent<InventoryManager>().floatingItem = null;
                }
            }
            else
            {
                GetComponentInParent<InventoryManager>().floatingItem = slotsItem;
                slotsItem = null;
                GetComponent<Image>().sprite = new Sprite();
            }
        }
    }
}