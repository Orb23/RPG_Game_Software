using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_box : MonoBehaviour
{

    public Sprite item_collected;
    private Inventory inv;
    public bool isItemCollected = false;
    public int box_id;
    public int item_id;

    void Awake()
    {
        LoadChest();
    }

    void Start()
    {
        inv = GameObject.FindObjectOfType(typeof(Inventory)) as Inventory;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveChest()
    {
        SaveMapSystem.SaveChest(this);
    }

    public void LoadChest()
    {
        //item's box_id is passed in order to load its corresponding save file.
        chestData data = SaveMapSystem.LoadChest(box_id);

        if(data == null)
        {
            //do nothing, file is not found to update item_box contents...
            // OR the player has entered the map scene for the first time.
            // hence, they should not have a chestdata{#}.data file.
        }
        else
        {
            //assigns this item_box to whatever is set on the file.
            isItemCollected = data.is_collected;
        }


        if (isItemCollected == true)
        {
            this.GetComponent<SpriteRenderer>().sprite = item_collected;
        }

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isItemCollected)
        {
            Debug.Log("Collecting Item");
            this.GetComponent<SpriteRenderer>().sprite = item_collected;
            isItemCollected = true;
            UpdateInventory(item_id);
        }
        else
        {
            Debug.Log("Item is already collected");
        }


    }

    void UpdateInventory(int item_id)
    {
        inv.incrementQuantity(item_id);
    }


    //whenever a scene is switches, everything is destroyed, thus
    // the last position will be saved before switching to the new scene.
    void OnDestroy()
    {
        SaveChest();
    }
}
