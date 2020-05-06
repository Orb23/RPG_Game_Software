using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter_BattleField : MonoBehaviour
{
    public Sprite battleComplete;
    public bool isComplete = false;
    public int event_id;

    void Awake()
    {
        LoadEncounterData();

        if(isComplete == true)
        {
            this.GetComponent<SpriteRenderer>().sprite = battleComplete;

            this.GetComponent<BoxCollider2D>().isTrigger = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }

        //do nothing
        // encounter can be triggered and sprite should not change picture.
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Touching");

        isComplete = true;
        this.GetComponent<SpriteRenderer>().sprite = battleComplete;

        this.GetComponent<BoxCollider2D>().isTrigger = false;

        SaveEncounterData();

        if(event_id == 1)
        {
            SceneManager.LoadScene(2);

        }
        else if(event_id == 2)
        {
            SceneManager.LoadScene(3);

        }
        else
        {
            SceneManager.LoadScene(4);
        }
    }


    //will ONLY be called when the player touches the collision of the encounterSprite.
    //  Called by OnTriggerEnter2D();
    public void SaveEncounterData()
    {
        SaveMapSystem.SaveEncounterData(this);
    }

    public void LoadEncounterData()
    {
        encounterData data = SaveMapSystem.LoadEncounterData(event_id);
        if (data == null)
        {
            //do nothing, file is not found to update player position...
            // OR the player has entered the map scene for the first time.
            // hence, they should not have a playerdata.data file.
        }
        else
        {
            //TODO: assign this data to that data.
            isComplete = data.isComplete;

        }

    }

}
