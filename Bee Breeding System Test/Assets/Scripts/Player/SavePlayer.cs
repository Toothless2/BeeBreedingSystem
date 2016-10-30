using UnityEngine;
using GameMaster;
using Inventory;

namespace SavePlayer
{
    public class SavePlayer : MonoBehaviour
    {
        [SerializeField]
        private GameObject inventory;
        private float waitTime;

        void Update()
        {
            if(Time.time > waitTime)
            {
                waitTime = Time.time + Random.Range(10.0f, 50.0f);
                UpdatePlayerSave();
            }
        }

        public void UpdatePlayerSave()
        {
            print(gameObject.name);
            AccesGameMaster.gameMaster.SavePlayer(gameObject.name, gameObject.transform, inventory.GetComponent<InventoryManager>().itemsCurrentlyInInventory);
        }
    }
}