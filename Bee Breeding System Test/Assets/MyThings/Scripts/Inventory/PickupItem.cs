using UnityEngine;
using System.Collections;
using Items;

namespace Inventory
{
    public class PickupItem : MonoBehaviour
    {
        public InventoryManager inventoryManager;
        public Transform objectInventory;

        void Update()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1);

            for (int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].transform.parent != this)
                {
                    if (colliders[i].GetComponentInParent<Item>() != null)
                    {
                        if(inventoryManager.itemsCurrentlyInInventory.Count + 1 <= inventoryManager.inventorySlots.Length)
                        {
                            colliders[i].transform.parent.SetParent(objectInventory);
                            inventoryManager.AddItemsToInventory();
                        }
                    }
                }
            }
        }
    }
}