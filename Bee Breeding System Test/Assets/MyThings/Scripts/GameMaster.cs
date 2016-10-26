using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using Bees.Genetics.Enums;
using Bees;
using Items;
using System.Collections.Generic;

namespace GameMaster
{
    public class GameMaster : MonoBehaviour
    {
        public Dictionary<string, ChestSave> chestSave = new Dictionary<string, ChestSave>();
        public Dictionary<string, ChestSave> chestLoad = new Dictionary<string, ChestSave>();

        private float waitTime = 10;

        void Start()
        {
            LoadChest();
        }
        
        void Update()
        {
            if(Time.time > waitTime)
            {
                waitTime = Time.time + Random.Range(10, 100);

                SaveChest();
                print("Saved");
            }
        }
        
        public void AddChestToSave(string chestName, Vector3 chestPosition, List<Item> chestItems)
        {
            //checks that the dict is not null
            if(chestSave != null)
            {
                //if the chest already has an entry remove it
                if (chestSave.ContainsKey(chestName))
                {
                    print("ItemRemoved");
                    chestSave.Remove(chestName);
                }
            }
            
            ChestSave chest = new ChestSave();
            //removes the numbers from the chest name
            chest = SaveChestPozAndName(chestName.Split(new char[] { ' ' })[0], chestPosition);

            //goes through each item in the chest
            foreach (var item in chestItems)
            {
                if (item.gameObject.GetComponent<Bee>() != null)
                {
                    chest.bee.Add(new SerializableBee(item.gameObject.GetComponent<Bee>()));
                }
            }

            chestSave.Add(chestName, chest);
        }

        //saves the dictionary
        void SaveChest()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.dataPath + "/Saves/testSave.dat", FileMode.OpenOrCreate);

            try
            {
                bf.Serialize(fs, chestSave);
            }
            catch (SerializationException e)
            {
                print("Failed to serialize: " + e);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        //Loads the saved dictionary into memory
        void LoadChest()
        {
            if(File.Exists(Application.dataPath + "/Saves/testSave.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();

                FileStream fs = new FileStream(Application.dataPath + "/Saves/testSave.dat", FileMode.Open);

                try
                {
                    chestLoad = (Dictionary<string, ChestSave>)bf.Deserialize(fs);
                    chestSave = chestLoad;
                }
                catch (SerializationException e)
                {
                    print("Failed to deserialize: " + e);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
                
                RemakeChests();
            }
        }

        void RemakeChests()
        {
            //Goes through each chest in the list
            foreach (var chest in chestLoad)
            {
                //makes the gameobject
                GameObject makeObject = Instantiate(ItemDictionary.AccesObjectDatabase(chest.Value.name));
                //sets the name to the key
                makeObject.name = chest.Key;
                //puts the object in the correct poz
                makeObject.transform.position = new Vector3(chest.Value.x, chest.Value.y, chest.Value.z);

                //goes through each item that is supposed to be in the chest
                foreach (var item in chest.Value.bee)
                {
                    //Makes the Bee Gameobject
                    GameObject bee = Instantiate(ItemDictionary.AccesObjectDatabase("Bee"));
                    //Gives the Bee data to the Bee
                    bee.GetComponent<Bee>().AssignVariables(item.slotIndex, item.species, item.temp);

                    bee.transform.parent = makeObject.GetComponentInChildren<Inventory.InventoryManager>().objectHolder.transform;
                }
            }
        }

        //saves chest Poz and Name
        ChestSave SaveChestPozAndName(string name, Vector3 poz)
        {
            ChestSave save = new ChestSave();

            save.name = name;
            save.x = poz.x;
            save.y = poz.y;
            save.z = poz.z;

            return save;
        }
    }

    //Serializable classes making life easier

    //Chest class
    [System.Serializable]
    public class ChestSave
    {
        public string name;
        public float x;
        public float y;
        public float z;

        public List<SerializableBee> bee = new List<SerializableBee>();
    }

    //Items
    [System.Serializable]
    public class SerialzableItem
    {
        public string name;
        public int? slotIndex;

        public SerialzableItem(string _itemName, int? _slotIndex)
        {
            name = _itemName;
            slotIndex = _slotIndex;
        }
    }

    //Bee
    [System.Serializable]
    public class SerializableBee
    {
        public string name;
        public int? slotIndex;
        public BeeSpecies species;
        public TempratureTolarence temp;

        private Bee _bee;

        public SerializableBee(Bee _bee)
        {
            name = _bee.itemName;
            slotIndex = _bee.slotindex;
            species = _bee.species;
            temp = _bee.temp;
        }
    }
}