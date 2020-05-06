using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class chestData
{
    public bool is_collected;
    public int unique_id_box;
    public int item_id;

    public chestData (item_box chest)
    {

        unique_id_box = chest.box_id;
        item_id = chest.item_id;
        is_collected = chest.isItemCollected;
    }

}
