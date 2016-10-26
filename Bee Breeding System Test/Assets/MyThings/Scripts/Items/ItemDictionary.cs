using UnityEngine;
using System.Collections.Generic;

namespace Items
{
    public class ItemDictionary : MonoBehaviour
    {
        public GameObject[] objectList;

        private static Dictionary<string, GameObject> objectDictionary = new Dictionary<string, GameObject>();

        //adds the items from the array into the dictionary
        void Awake()
        {
            foreach (var item in objectList)
            {
                objectDictionary.Add(item.name, item);
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
