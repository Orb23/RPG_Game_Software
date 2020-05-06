using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public int[] inventory = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

    public Text[] totals;

    //Testing item usage
    //public int[] inventory;

    public static Inventory instance;

    private BattleSystem battleSystem;


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(transform.root.gameObject);

        //test function to fill inventory
        fillInventory(2);
    }

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            totals[i].text = "" + inventory[i];
        }
    }

    public void fillInventory(int fill)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = fill;
        }
    }

    //returns true if there was an item left to decrement
    public bool decItem (int i_id)
    {
        if(inventory[i_id] > 0)
        {
            inventory[i_id]--;
            Debug.Log(inventory[i_id] + " items remaining.");
            return true;
        }
        else
        {
            inventory[i_id] = 0;
            Debug.Log("Not enough item!");
            return false;
        }

    }

    public void incItem (int i_id)
    {
        if(inventory[i_id] < 99)
        {
            inventory[i_id]++;
            Debug.Log(inventory[i_id] + " items remaining.");
        }
    }

    //pass an action to battlesystem
    public void passAction(int i_id)
    {
        battleSystem = GameObject.FindObjectOfType(typeof(BattleSystem)) as BattleSystem;
        
        //only try to perform an action when in a battle scene
        if (battleSystem != null)
        {
            Debug.Log("BattleSystem found!");
            if (battleSystem.turn_state != TurnState.MENU && battleSystem.turn_state != TurnState.MIX)
            {
                Debug.Log("Bad timing error.");
                return;
            }
            //take item from player inventory if usable
            if (battleSystem.state == BattleState.PLAYERTURN)
            {
                if (decItem(i_id) != true)
                {
                    //stop not enough of an item
                    return;
                }
            }

            action new_act = new action("error", 0, false, 0, 0, 0, false);
            //set the action based on the item id passed
            switch (i_id)
            {
                //healing
                case 0:
                    new_act = new action("potion", 30, false, 0, 0, 0, false); break;
                case 1:
                    new_act = new action("large potion", 60, false, 0, 0, 0, false); break;
                case 2:
                    new_act = new action("bandage", 15, false, 0, 1, 0, false); break;
                case 3:
                    new_act = new action("antidote", 10, false, 1, 0, 0, false); break;
                case 4:
                    new_act = new action("limber needle", 5, false, 0, 0, 1, false); break;
                case 5:
                    new_act = new action("holy water", 20, false, 1, 1, 1, false); break;
                case 6:
                    new_act = new action("restoration", 40, false, 0, 0, 0, true); break;
                //weapons
                case 7:
                    new_act = new action("bomb", 30, true, 0, 0, 0, false); break;
                case 8:
                    new_act = new action("dynamite", 60, true, 0, 0, 0, false); break;
                case 9:
                    new_act = new action("bloody dagger", 20, true, 0, 1, 0, false); break;
                case 10:
                    new_act = new action("poison chalice", 10, true, 1, 0, 0, false); break;
                case 11:
                    new_act = new action("stiff needle", 10, true, 0, 0, 1, false); break;
                case 12:
                    new_act = new action("bad omen", 10, true, 2, 2, 2, false); break;
                case 13:
                    new_act = new action("spite bomb", 40, true, 0, 0, 0, true); break;
                
                default: break;
            }
            //pass the new action to the battle
            battleSystem.VerifyAction(new_act);
        }
        else
        {
            Debug.Log("BattleSystem not found...");
        }

    }

    public void isItemPresent(int item_id)
    {

    }


    public void decrementQuantity(item item)
    {
        int array_pos = item.item_id - 1;

        Debug.Log("You have: " + inventory[array_pos]);
        if(inventory[array_pos] <= 0)
        {
            //do nothing.
        }
        else
        {
            --inventory[array_pos];
            Debug.LogWarning("Used item, Remaining: " + inventory[array_pos]);
        }        

    }

    public void incrementQuantity(int item_id)
    {
        int array_pos = item_id - 1;

        ++inventory[array_pos];
        Debug.Log("You now have a total of: " + inventory[array_pos]);

    }

    void SaveInventory()
    {
        SaveMapSystem.SaveInventory(this);
    }
    
    /*
    void LoadInventory()
    {
        inventoryData data = SaveMapSystem.LoadInventory();
        if (data == null)
        {
            //do nothing, file is not found to update player position...
            // OR the player has entered the map scene for the first time.
            // hence, they should not have a playerdata.data file.
        }
        else
        {
            inventory[0] = data.inventory[0];
            inventory[1] = data.inventory[1];
            inventory[2] = data.inventory[2];
            inventory[3] = data.inventory[3];
            inventory[4] = data.inventory[4];
            inventory[5] = data.inventory[5];
            inventory[6] = data.inventory[6];
            inventory[7] = data.inventory[7];
            inventory[8] = data.inventory[8];
            inventory[9] = data.inventory[9];
            inventory[10] = data.inventory[10];
            inventory[11] = data.inventory[11];
            inventory[12] = data.inventory[12];
            inventory[13] = data.inventory[13];
        }

    }
    */

    //Saves the current inventory whenever it leaves the scene.
    //Saves to an inventory.datas file that can be used anywhere on the map.
    //

    /*
    void OnDestroy()
    {
        gameObject.SetActive(true);

        SaveInventory();
        gameObject.SetActive(false);
    }
    */

}
