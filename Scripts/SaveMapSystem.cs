using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveMapSystem
{
    public static void SavePlayer (grid_move player)
    {
        Debug.Log(Directory.GetCurrentDirectory());
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Directory.GetCurrentDirectory() + "/playermap.datas";
        FileStream stream = new FileStream(path, FileMode.Create);


        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        Debug.Log(Directory.GetCurrentDirectory());


        string path = Directory.GetCurrentDirectory() + "/playermap.datas";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }


    // SaveChest takes in the properties of item_box from item_box.cs
    // Checks the box's id number to save into its own chestdata file.
    // NOTE: Testing will only allow up to three chests to be saved, any more chest
    // on the map will not work.
    //  To fix NOTE, a better save function will be needed.
    public static void SaveChest(item_box chest)
    {

        if (chest.box_id == 1)
        {
            Debug.Log("Creating chestdata1.datas");
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Directory.GetCurrentDirectory() + "/chestdata1.datas";
            FileStream stream = new FileStream(path, FileMode.Create);


            chestData data = new chestData(chest);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        else if (chest.box_id == 2)
        {
            Debug.Log("Creating chestdata2.datas");
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Directory.GetCurrentDirectory() + "/chestdata2.datas";
            FileStream stream = new FileStream(path, FileMode.Create);


            chestData data = new chestData(chest);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        else if (chest.box_id == 3)
        {
            Debug.Log("Creating chestdata3.datas");
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Directory.GetCurrentDirectory() + "/chestdata3.datas";
            FileStream stream = new FileStream(path, FileMode.Create);


            chestData data = new chestData(chest);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            Debug.LogError("Could not Save chest.");
        }

    }

    // Takes in box's id number from item_box.cs
    public static chestData LoadChest(int chestID)
    {

        if(chestID == 1)
        {
            Debug.Log("Accessing chestdata1.datas");
            string path = Directory.GetCurrentDirectory() + "/chestdata1.datas";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                chestData data = formatter.Deserialize(stream) as chestData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogWarning("Save file not found in " + path);
                return null;
            }

        }
        else if(chestID == 2)
        {
            Debug.Log("Accessing chestdata2.datas");
            string path = Directory.GetCurrentDirectory() + "/chestdata2.datas";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                chestData data = formatter.Deserialize(stream) as chestData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogWarning("Save file not found in " + path);
                return null;
            }

        }
        else if(chestID == 3)
        {
            Debug.Log("Accessing chestdata3.datas");
            string path = Directory.GetCurrentDirectory() + "/chestdata3.datas";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                chestData data = formatter.Deserialize(stream) as chestData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogWarning("Save file not found in " + path);
                return null;
            }
        }
        else
        {
            Debug.LogError("Could not load chest number: " + chestID);
            return null;
        }

    }



    public static void SaveEncounterData(Enter_BattleField event_data)
    {

        if (event_data.event_id == 1)
        {
            Debug.Log("Creating eventdata1.datas");
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Directory.GetCurrentDirectory() + "/eventdata1.datas";
            FileStream stream = new FileStream(path, FileMode.Create);


            encounterData data = new encounterData(event_data);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        else if (event_data.event_id == 2)
        {
            Debug.Log("Creating eventdata2.datas");
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Directory.GetCurrentDirectory() + "/eventdata2.datas";
            FileStream stream = new FileStream(path, FileMode.Create);


            encounterData data = new encounterData(event_data);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        else if (event_data.event_id == 3)
        {
            Debug.Log("Creating eventdata3.datas");
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Directory.GetCurrentDirectory() + "/eventdata3.datas";
            FileStream stream = new FileStream(path, FileMode.Create);


            encounterData data = new encounterData(event_data);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            Debug.LogError("Could not Save encounterData(Enter_BattleField.cs).");
        }

    }

    public static encounterData LoadEncounterData(int eventID)
    {

        if (eventID == 1)
        {
            Debug.Log("Accessing eventdata1.datas");
            string path = Directory.GetCurrentDirectory() + "/eventdata1.datas";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                encounterData data = formatter.Deserialize(stream) as encounterData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogWarning("Save file not found in " + path);
                return null;
            }

        }
        else if (eventID == 2)
        {
            Debug.Log("Accessing eventdata2.datas");
            string path = Directory.GetCurrentDirectory() + "/eventdata2.datas";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                encounterData data = formatter.Deserialize(stream) as encounterData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogWarning("Save file not found in " + path);
                return null;
            }

        }
        else if (eventID == 3)
        {
            Debug.Log("Accessing eventdata3.datas");
            string path = Directory.GetCurrentDirectory() + "/eventdata3.datas";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                encounterData data = formatter.Deserialize(stream) as encounterData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogWarning("Save file not found in " + path);
                return null;
            }

        }
        else
        {
            Debug.LogError("Could not load encounterData(eventID) number: " + eventID);
            return null;
        }

    }


    //TODO:
    // SAVE AND LOAD SYSTEM FOR INVENTORY 
    // simple array of 14 space.
    //load is called from battlesystem upon awake()
    //

    public static void SaveInventory(Inventory inventory)
    {
        Debug.Log(Directory.GetCurrentDirectory());
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Directory.GetCurrentDirectory() + "/inventory.datas";
        FileStream stream = new FileStream(path, FileMode.Create);


        inventoryData data = new inventoryData(inventory);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static inventoryData LoadInventory()
    {
        Debug.Log(Directory.GetCurrentDirectory());


        string path = Directory.GetCurrentDirectory() + "/inventory.datas";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            inventoryData data = formatter.Deserialize(stream) as inventoryData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }


}
