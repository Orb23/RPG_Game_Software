using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class encounterData
{ 
    public bool isComplete;
    public int event_id;

    public encounterData(Enter_BattleField event_data)
    {
        isComplete = event_data.isComplete;
        event_id = event_data.event_id; 
    }


}
