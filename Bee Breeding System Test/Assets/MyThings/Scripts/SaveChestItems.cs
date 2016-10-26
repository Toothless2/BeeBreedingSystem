using UnityEngine;
using System.Collections.Generic;
using GameMaster;
using Items;

namespace Inventory
{
    public class SaveChestItems : MonoBehaviour
    {
        private float waitTime;
        public InventoryManager chestInventory;
        [SerializeField]
        private int prevListCount;

        void Start()
        {
            chestInventory = gameObject.GetComponentInChildren<InventoryManager>();
            Debug.Assert(chestInventory != null);

            prevListCount = chestInventory.itemsCurrentlyInInventory.Count;
        }
        
        void Update()
        {
            //When item Added/Removed from the inventory it is updated
            if(prevListCount != chestInventory.itemsCurrentlyInInventory.Count)
            {
                AccesGameMaster.gameMaster.AddChestToSave(gameObject.name, transform.position, chestInventory.itemsCurrentlyInInventory);
                prevListCount = chestInventory.itemsCurrentlyInInventory.Count;
            }
        }
    }
}
