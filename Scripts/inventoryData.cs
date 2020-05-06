using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class inventoryData
{
    public int[] inventory = new int[14];

    public inventoryData(Inventory inven)
    {
        inventory[0] = inven.inventory[0];
        inventory[1] = inven.inventory[1];
        inventory[2] = inven.inventory[2];
        inventory[3] = inven.inventory[3];
        inventory[4] = inven.inventory[4];
        inventory[5] = inven.inventory[5];
        inventory[6] = inven.inventory[6];
        inventory[7] = inven.inventory[7];
        inventory[8] = inven.inventory[8];
        inventory[9] = inven.inventory[9];
        inventory[10] = inven.inventory[10];
        inventory[11] = inven.inventory[11];
        inventory[12] = inven.inventory[12];
        inventory[13] = inven.inventory[13];
    }



}
