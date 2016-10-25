using UnityEngine;
using System.Collections;
using GameMaster;
using Items;

namespace Inventory
{
    public class SaveChestItems : MonoBehaviour
    {
        private float waitTime;
        public InventoryManager chestInventory;

        void Start()
        {
            chestInventory = gameObject.GetComponentInChildren<InventoryManager>();
            chestInventory.gameObject.SetActive(false);
            Debug.Assert(chestInventory != null);
            AccesGameMaster.gameMaster.AddChestToSave(gameObject.name, transform.position, chestInventory.itemsCurrentlyInInventory);
        }
        
        //TODO Resave then things added to chest
    }
}
