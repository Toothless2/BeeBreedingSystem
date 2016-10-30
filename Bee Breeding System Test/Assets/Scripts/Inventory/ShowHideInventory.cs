using UnityEngine;

namespace Inventory
{
    public class ShowHideInventory : MonoBehaviour
    {
        public GameObject inventory;
        private bool invOpen;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.E) && !invOpen)
            {
                invOpen = true;
                inventory.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if(Input.GetKeyDown(KeyCode.E) && invOpen)
            {
                invOpen = false;
                inventory.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
