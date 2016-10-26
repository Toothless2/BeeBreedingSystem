using UnityEngine;
using Items;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public int test;
        public GameObject slotHolder;
        public GameObject[] inventorySlots;
        public GameObject objectHolder;
        public List<Item> itemsCurrentlyInInventory;
        public Item floatingItem;

        void IndexSlots()
        {
            //Gives each inventory slot an index
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i].GetComponent<InventorySlot>().slotIndex = i;
            }

            AddItemsToInventory();
        }

        public void AddItemsToInventory()
        {
            //clears the list
            itemsCurrentlyInInventory.Clear();

            //add each item to the inventory list
            for (int h = 0; h < objectHolder.transform.childCount; h++)
            {
                itemsCurrentlyInInventory.Add(objectHolder.transform.GetChild(h).GetComponent<Item>());
            }
            
            //gives each item a slot in the inventory
            foreach (var item in itemsCurrentlyInInventory)
            {
                item.transform.localPosition = new Vector3(0, 0, 0);

                if (item.slotindex != null)
                {
                    inventorySlots[(int)item.slotindex].GetComponent<InventorySlot>().slotsItem = item;
                }
                else
                {
                    for (int h = 0; h < inventorySlots.Length; h++)
                    {
                        if (inventorySlots[h].GetComponent<InventorySlot>().slotsItem == null)
                        {
                            inventorySlots[h].GetComponent<InventorySlot>().slotsItem = item;
                            item.slotindex = h;
                            break;
                        }
                    }
                }
            }
        }
        
        void Update()
        {
            test++;
            AddItemsToInventory();

            //find the tiem clicked on and moves it to floating items
            if (Input.GetMouseButton(1) && floatingItem != null)
            {
                itemsCurrentlyInInventory.RemoveAt((int)floatingItem.slotindex);
                floatingItem.slotindex = null;
                floatingItem.GetComponent<Transform>().position = transform.forward * 3;
                floatingItem.gameObject.transform.parent = null;
                floatingItem = null;
            }
        }
    }
}
