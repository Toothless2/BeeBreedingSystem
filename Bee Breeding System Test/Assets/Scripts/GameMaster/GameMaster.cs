using UnityEngine;
using System.Threading;
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
        private Dictionary<string, ChestSave> chestSave = new Dictionary<string, ChestSave>();
        private Dictionary<string, ChestSave> chestLoad = new Dictionary<string, ChestSave>();

        private Dictionary<string, SavePlayer> playerSave = new Dictionary<string, SavePlayer>();
        private Dictionary<string, SavePlayer> playerLoad = new Dictionary<string, SavePlayer>();

        private float waitTime = 10;

        void Start()
        {
            LoadChest();
            LoadPlayer();
        }
        
        void Update()
        {
            if(Time.time > waitTime)
            {
                waitTime = Time.time + Random.Range(10, 100);

                string _path = Application.dataPath;

                Thread chests = new Thread(() => SaveChest(chestSave, _path));
                Thread player = new Thread(() => SavePlayer(playerSave, _path));
                chests.Start();
                player.Start();

                print("Saved");
            }
        }

        #region SaveItems
        public void SavePlayer(string playerName, Transform playerTransform, List<Item> playerInventory)
        {
            if(playerSave.ContainsKey(playerName))
            {
                playerSave.Remove(playerName);
            }

            SavePlayer playerData = new SavePlayer();

            playerData.SavePoz(playerTransform.position);
            playerData.SaveRot(new Vector3(playerTransform.rotation.x, playerTransform.rotation.y, playerTransform.rotation.z));

            foreach (var item in playerInventory)
            {
                if (item.gameObject.GetComponent<Bee>() != null)
                {
                    playerData.bees.Add(new SerializableBee(item.gameObject.GetComponent<Bee>()));
                }
                else if (item.gameObject.GetComponent<Item>() != null)
                {
                    playerData.items.Add(new SerialzableItem(item.gameObject.GetComponent<Item>()));
                }
            }

            playerSave.Add(playerName, playerData);
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
                    chest.bees.Add(new SerializableBee(item.gameObject.GetComponent<Bee>()));
                }
                else if (item.gameObject.GetComponent<Item>() != null)
                {
                    chest.items.Add(new SerialzableItem(item.gameObject.GetComponent<Item>()));
                }
            }

            chestSave.Add(chestName, chest);
        }

        void SavePlayer(Dictionary<string, SavePlayer> _playerSave, string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path + "/Saves/Players.dat", FileMode.OpenOrCreate); 

            try
            {
                bf.Serialize(fs, _playerSave);
            }
            catch(SerializationException e)
            {
                print("Failed to Deserialize Player: " + e);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        //saves the dictionary
        void SaveChest(Dictionary<string, ChestSave> _saveChests, string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path + "/Saves/Chests.dat", FileMode.OpenOrCreate);

            try
            {
                bf.Serialize(fs, _saveChests);
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
        #endregion SaveItems

        #region LoadItems
        //Loads the saved dictionary into memory
        void LoadChest()
        {
            if(File.Exists(Application.dataPath + "/Saves/Chests.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();

                FileStream fs = new FileStream(Application.dataPath + "/Saves/Chests.dat", FileMode.Open);

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

        void LoadPlayer()
        {
            if(File.Exists(Application.dataPath + "/Saves/Players.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();

                FileStream fs = new FileStream(Application.dataPath + "/Saves/Players.dat", FileMode.Open);

                try
                {
                    playerLoad = (Dictionary<string, SavePlayer>)bf.Deserialize(fs);
                    playerSave = playerLoad;
                }
                catch(SerializationException e)
                {
                    Debug.LogWarning("Failed to Desierialize player: " + e);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }

            RemakePlayer();
        }
        #endregion LoadItems

        #region RemakeObjects 
        void RemakePlayer()
        {
            foreach (var player in playerLoad)
            {
                GameObject makeobject = Instantiate(ItemDictionary.AccesObjectDatabase("Player"));

                makeobject.name = player.Key;

                makeobject.transform.rotation = Quaternion.Euler(new Vector3(player.Value.rotX, player.Value.rotY, player.Value.rotZ));
                makeobject.transform.position = new Vector3(player.Value.pozX, player.Value.pozY, player.Value.pozZ);

                foreach (var item in player.Value.bees)
                {
                    GameObject bee = RemakeBeeItem(item);

                    bee.transform.parent = makeobject.GetComponent<Inventory.ShowHideInventory>().inventory.GetComponent<Inventory.InventoryManager>().objectHolder.transform;
                }
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
                foreach (var item in chest.Value.bees)
                {
                    //Makes the Bee Gameobject
                    GameObject bee = RemakeBeeItem(item);

                    bee.transform.parent = makeObject.GetComponentInChildren<Inventory.InventoryManager>().objectHolder.transform;
                }
            }
        }
        #endregion RemakeObjects

        GameObject RemakeBeeItem(SerializableBee bee)
        {
            GameObject _bee = Instantiate(ItemDictionary.AccesObjectDatabase("Bee"));
            _bee.GetComponent<Bee>().AssignVariables(bee.slotIndex, bee.species, bee.temp);

            return _bee;
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

    #region SerializableClasses
    //Serializable classes making life easier
    [System.Serializable]
    public class SavePlayer
    {
        public string playerName;

        public float pozX;
        public float pozY;
        public float pozZ;

        public float rotX;
        public float rotY;
        public float rotZ;

        public List<SerializableBee> bees = new List<SerializableBee>();
        public List<SerialzableItem> items = new List<SerialzableItem>();

        public void SavePoz(Vector3 poz)
        {
            pozX = poz.x;
            pozY = poz.y;
            pozZ = poz.z;
        }

        public void SaveRot(Vector3 rot)
        {
            rotX = rot.x;
            rotY = rot.y;
            rotZ = rot.z;
        }
    }

    //Chest class
    [System.Serializable]
    public class ChestSave
    {
        public string name;
        public float x;
        public float y;
        public float z;

        public List<SerializableBee> bees = new List<SerializableBee>();
        public List<SerialzableItem> items = new List<SerialzableItem>();
    }

    //Items
    [System.Serializable]
    public class SerialzableItem
    {
        public string name;
        public int? slotIndex;

        public SerialzableItem(Item _item)
        {
            name = _item.itemName;
            slotIndex = _item.slotindex;
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
    #endregion SerializableClasses
}