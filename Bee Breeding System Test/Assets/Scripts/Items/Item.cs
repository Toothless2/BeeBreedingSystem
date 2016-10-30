using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class Item : MonoBehaviour
    {
        public string itemName;
        public int? slotindex;
        public GameObject itemGameObject;
        public Sprite itemSprite;

        void Update()
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}