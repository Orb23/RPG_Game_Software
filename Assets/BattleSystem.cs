using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//a simple set of states that the battlesystem can use.
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public BattleState state;

    public GameObject playerPrefab;

        /*
    public GameObject thiefPrefab;
    public GameObject mixerPrefab;
    public GameObject dps_charPrefab;
    */

    public GameObject enemyPrefab;

    public Transform playerBattleStation_1;
    //public Transform playerBattleStation_2;
    //public Transform playerBattleStation_3;

    public Transform enemyBattleStation_1;
    //public Transform enemyBattleStation_2;
    //public Transform enemyBattleStation_3;

    public BattleHud playerHUD;
    public BattleHud enemyHUD;


    /*
    //Unit is going to be replaced with thief, mixer, dps
    DPS_Player dps_charUnit;
    Mixer mixerUnit;
    Thief thiefUnit;
    */

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        
    }

    IEnumerator SetupBattle()
    {
        //Instantiate creates the object onto the screen.
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation_1);
        playerUnit = playerGO.GetComponent<Unit>();

        //GameObject mixerGO = Instantiate(mixerPrefab, playerBattleStation_3);
        //mixerUnit = mixerGO.GetComponent<Mixer>();


        //GameObject thiefGO = Instantiate(thiefPrefab, playerBattleStation_2);
        //thiefUnit = thiefGO.GetComponent<Thief>();

        //GameObject dps_charGO = Instantiate(dps_charPrefab, playerBattleStation_1);
        //dps_charUnit = dps_charGO.GetComponent<DPS_Player>();


        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation_1);
        enemyUnit = enemyGO.GetComponent<Unit>();


        dialogueText.text = "Enemy " + enemyUnit.unitName + " has appeared!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";


        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            //end battle
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            //enemy turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }


    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";

        }else if(state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";

    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You have healed!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }



    //


}
