using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win_game_script : MonoBehaviour
{
    private bool event_1_check;
    private bool event_2_check;
    private bool event_3_check;


    void Start()
    {
        event_1_check = GameObject.Find("Enemy_Grid_1").GetComponent<Enter_BattleField>().isComplete;
        event_2_check = GameObject.Find("Enemy_Grid_2").GetComponent<Enter_BattleField>().isComplete;
        event_3_check = GameObject.Find("Enemy_Grid_3").GetComponent<Enter_BattleField>().isComplete;
    }

    void Update()
    {
        if(event_1_check && event_2_check && event_3_check)
        {
            SceneManager.LoadScene(5);
        }
        else
        {
            //null do nothing
            //player still have events to clear in order to win the game.
        }
    }

}
