using UnityEngine;
using System.Collections.Generic;

namespace Items
{
    public class ItemDictionary : MonoBehaviour
    {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public GameObject[] objectList;

        private static Dictionary<string, GameObject> objectDictionary = new Dictionary<string, GameObject>();

        //adds the items from the array into the dictionary
        void Awake()
        {
            stopwatch.Reset();
            stopwatch.Start();

            foreach (var item in objectList)
            {
                if(!objectDictionary.ContainsKey(item.name))
                {
                    objectDictionary.Add(item.name, item);
                }
            }

            stopwatch.Stop();
            Debug.Log("Time to complete getting items: " + stopwatch.ElapsedMilliseconds);
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
