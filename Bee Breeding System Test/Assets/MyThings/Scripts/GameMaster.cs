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
            print(chestSave.Count);
            if(Time.time > waitTime)
            {
                print("Saved");
                waitTime = Time.time + Random.Range(100, 500);

                SaveChest();
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
                    chestSave.Remove(chestName);
                }
            }
            
            ChestSave chest = new ChestSave();
            
            chest = SaveChestPozAndName(chestName.Split(new char[] { ' ' })[0], chestPosition);

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
            foreach (var chest in chestLoad)
            {
                GameObject makeObject = Instantiate(ItemDictionary.AccesObjectDatabase(chest.Value.name));
                makeObject.name = chest.Key;
                makeObject.transform.position = new Vector3(chest.Value.x, chest.Value.y, chest.Value.z);

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

    [System.Serializable]
    public class ChestSave
    {
        public string name;
        public float x;
        public float y;
        public float z;

        public List<SerializableBee> bee = new List<SerializableBee>();
    }

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