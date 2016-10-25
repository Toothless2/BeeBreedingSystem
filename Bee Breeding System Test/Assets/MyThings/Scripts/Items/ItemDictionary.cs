using UnityEngine;
using System.Collections.Generic;

namespace Items
{
    public class ItemDictionary : MonoBehaviour
    {
        [System.Serializable]
        public struct ItemHolder
        {
            public Item itemScript;
        }

        public GameObject[] objectList;
        public ItemHolder[] itemList;

        private static Dictionary<string, GameObject> objectDictionary = new Dictionary<string, GameObject>();
        private static Dictionary<string, Item> itemsDictionary = new Dictionary<string, Item>();

        //adds the items from the array into the dictionary
        void Awake()
        {
            foreach (var item in objectList)
            {
                objectDictionary.Add(item.name, item);
            }

            foreach (ItemHolder item in itemList)
            {
                itemsDictionary.Add(item.itemScript.itemName, item.itemScript);
            }
        }

        //Allows the Users to Acces the Database but not add anything at runtime
        public static Item AccesItemDatabase(string itemName)
        {
            try
            {
                return itemsDictionary[itemName];
            }
            catch
            {
                return null;
            }
        }

        //Gets objects in the game;
        public static GameObject AccesObjectDatabase(string objectName)
        {
            try
            {
                return objectDictionary[objectName];
            }
            catch
            {
                return null;
            }
        }
    }
}
