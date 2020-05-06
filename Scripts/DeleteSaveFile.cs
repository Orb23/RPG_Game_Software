using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeleteSaveFile : MonoBehaviour
{
    public void deleteData()
    {
        Debug.Log("test");
        Debug.Log(Directory.GetCurrentDirectory());
        string[] data = { "/chestdata1", "/chestdata2", "/chestdata3", "/playermap", "/eventdata1", "/eventdata2", "/eventdata3" };


        for(int i = 0; i < 7; i++)
        {
            string path = Directory.GetCurrentDirectory() + data[i]+ ".datas";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Debug.LogWarning("Save File not found in: " + path);
            }

        }
    }


}
