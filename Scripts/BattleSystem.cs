using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//a simple set of states that the battlesystem can use.
public enum BattleState { START, BATTLE, PLAYERTURN, ENEMYTURN, WON, LOST }
public enum TurnState { START, MENU, MIX, TARGET, ACTION, WAIT, END, FINISH }

//TODO: 
//  create an action queue that both player and enemy A.I will use
//  for their prep and execution phases.

//public string actionQueue = [];

/// CLASS TO HANDLE ACTIONS WHICH WILL BE APPLIED TO UNITS (ALSO FUNCTIONS AS THE ITEM CLASS)
public class action
{
    public string name = "";
    public int damage = 0; //number of damage/health change
    public bool hurt = false; //boolean if action will heal/recover status or damage, false means healing, true means damaging action
    public int poison = 0; //number of ticks of poison changed by action
    public int bleed = 0; //number of ticks of bleed changed by action
    public int paralyze = 0; //number of ticks of paralysis changed by action
    public bool recover = false; //boolean if ally(heal) or item (damage) will be recovered by action

    ///actions(item) combined to create current action
    //for the thief, this will be used to steal an item by "recovering" whatever the targeted enemies item is and listing it in item1
    //the item mixer will list both items used in a mix in the item1 and item2 variables so they can be listed/refered to when needed
    //the doctor's buffed attacks will include the items used so they can be listed/refered to when needed 
    action item1 = null;
    action item2 = null;
    action item3 = null;

    //constructor to create new actions on the fly for debug or other purposes
    public action(string NAME = "", int DAMAGE = 0, bool HURT = false, int POISON = 0, int BLEED = 0, int PARALYZE = 0, bool RECOVER = false, action I1 = null, action I2 = null, action I3 = null)
    {
        name = NAME;
        damage = DAMAGE;
        hurt = HURT;
        poison = POISON;
        bleed = BLEED;
        paralyze = PARALYZE;
        recover = RECOVER;

        item1 = I1;
        item2 = I2;
        item3 = I3;
    }
    //copy constructor used for item mixer or doctor to fuse actions (need to make some variable to determine which is which)
    //as a note, a buffed attack from the doctor will be the end result of mixing several actions in a row
    public action(string NAME, action a1, action a2)
    {
        name = NAME; //can't really combine names unless you're crazy
        damage = a1.damage + a2.damage; //add damages together
        if (a1.hurt || a2.hurt) { hurt = true; } else { hurt = false; } //set to true if any action had true
        poison = a1.poison + a2.poison; //add poison ticks
        bleed = a1.bleed + a2.bleed; //add bleed ticks
        paralyze = a1.paralyze + a2.paralyze; //add paralysis ticks
        if (a1.recover || a2.recover) { recover = true; } else { recover = false; } //set to true if any action had true

        //only store the item if the item had a name, otherwise discard the item
        if (a1.name != "") { item1 = a1; } else { item1 = null; }
        if (a2.name != "") { item2 = a2; } else { item2 = null; }
    }
}

//!!!!!!!!!!!!!!!!!!!!!!
// public unit class has been moved to Unit.cs
// Each prefab in Assets > Prefab > Battle Prefab is using the Unit.cs script !!!
//
//!!!!!!!!!!!!!


public class BattleSystem : MonoBehaviour
{

    public BattleState state;
    public TurnState turn_state;

    
    public GameObject thiefPrefab;
    public GameObject mixerPrefab;
    public GameObject healerPrefab;

    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;

    public Transform playerBattleStation_1;
    public Transform playerBattleStation_2;
    public Transform playerBattleStation_3;

    public Transform enemyBattleStation_1;
    public Transform enemyBattleStation_2;
    public Transform enemyBattleStation_3;

    Unit playerUnit;
    Unit healerUnit;
    Unit mixerUnit;
    Unit thiefUnit;
    Unit enemyUnit1;
    Unit enemyUnit2;
    Unit enemyUnit3;

    public Text dialogueText;

    public Unit[] turns;
    public int unitID = 0; // the id of the unit whose turn is up/next
    public Unit currUnit = null;    // the game object currently having their turn, set null to end turn

    private action turn_action = null;
    private action mix_action = null;
    public Unit turn_target = null;

    private Inventory inv;


    /*
    public bool inventoryEnabled;
    public GameObject inventory;
    public GameObject itemHolder;
    */


    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        
    }

    IEnumerator SetupBattle()
    {
        //Instantiate creates the object onto the screen.
        //GameObject playerGO = Instantiate(playerPrefab, playerBattleStation_1);
        //playerUnit = playerGO.GetComponent<Unit>();

        inv = GameObject.FindObjectOfType(typeof(Inventory)) as Inventory;

        turns = new Unit[6];

        GameObject thiefGO = Instantiate(thiefPrefab, playerBattleStation_3);
        thiefUnit = thiefGO.GetComponent<Unit>();
        turns[0] = thiefUnit;
        
        GameObject mixerGO = Instantiate(mixerPrefab, playerBattleStation_2);
        mixerUnit = mixerGO.GetComponent<Unit>();
        turns[1] = mixerUnit;

        GameObject healerGO = Instantiate(healerPrefab, playerBattleStation_1);
        healerUnit = healerGO.GetComponent<Unit>();
        turns[2] = healerUnit;

        GameObject enemyGO1 = Instantiate(enemyPrefab1, enemyBattleStation_3);
        enemyUnit1 = enemyGO1.GetComponent<Unit>();
        turns[3] = enemyUnit1;

        GameObject enemyGO2 = Instantiate(enemyPrefab2, enemyBattleStation_2);
        enemyUnit2 = enemyGO2.GetComponent<Unit>();
        turns[4] = enemyUnit2;

        GameObject enemyGO3 = Instantiate(enemyPrefab3, enemyBattleStation_1);
        enemyUnit3 = enemyGO3.GetComponent<Unit>();
        turns[5] = enemyUnit3;


        dialogueText.text = "Enemy " + enemyUnit1.unitName + " has appeared!";

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
    }

    void checkEnd()
    {
        if (turns[3].dead && turns[4].dead && turns[5].dead)
        {
            state = BattleState.WON;
            currUnit = turns[0];
            display_message("Enemies defeated!");
            StartCoroutine(DelayTurn(TurnState.FINISH, 3f));
        }
        if (turns[0].dead && turns[1].dead && turns[2].dead)
        {
            state = BattleState.LOST;
            currUnit = turns[0];
            display_message("You were defeated...");
            StartCoroutine(DelayTurn(TurnState.FINISH, 3f));
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
    }

    ////Connected through:   Battle_UI_Canvas > DialoguePanel > CombatButtons > Attack On Click()
    public void OnAttackButton()
    {
        action attack_act = currUnit.GenerateAttack();
        if (currUnit == turns[0])
        {
            //create a new action if it's the theifs turn
            attack_act = new action("theft", attack_act, new action("", 0, true, 0,0,0, true));
        }
        VerifyAction(attack_act);
    }

    public void VerifyAction(action new_act)
    {
        //only accept actions during the menu/mix phase
        if (turn_state != TurnState.MENU && turn_state != TurnState.MIX)
        {
            return;
        }
        //if no problems, accept new action
        if (turn_action == null)
        {
            turn_action = new_act;
        }
    }

        public void TargetSelect(Unit new_target)
    {
        //only accept actions during the target phase
        if (turn_state != TurnState.TARGET)
        {
            return;
        }
        //if no problems, generate an attack action for the unit and display message
        turn_target = new_target;
    }

    IEnumerator DelayTurn(TurnState new_state, float time)
    {
        turn_state = TurnState.WAIT;
        yield return new WaitForSeconds(time);
        turn_state = new_state;
    }


    //
    void Update()
    {
        // manage changing turns
		if (currUnit == null) // if no active turn
		{
			// select the active unit
			currUnit = turns[unitID];
            turn_state = TurnState.START;
            turn_action = null;

            // signal if player should have control or not
            if (unitID <= 2) {state = BattleState.PLAYERTURN;} else {state = BattleState.ENEMYTURN;}

                        // move on the unit count so next time we pick the next player
			unitID++;
            // loop back to the first unit when turn over
			if (unitID >= turns.Length) unitID = 0;
        }

        if (currUnit != null) // if active turn found
        {
            switch (turn_state) 
            {
            case TurnState.START:
                ///manage pre-turn events
                //poison is dealt to unit at start of turn
                if (currUnit.poison != 0)
                {
                    display_message( currUnit.unitName + " was hurt by poison!");
                    currUnit.TakeAction( new action("Poison Damage", 5*currUnit.poison, true) );
                }

                //paralysis
                if (currUnit.paralyze == 3)
                {
                    //reduce paralysis and skip turn
                    display_message( currUnit.unitName + " couldn't move!");
                    currUnit.paralyze = 1;
                    currUnit = null;
                }
                //transition to next phase of turn
                StartCoroutine(DelayTurn(TurnState.MENU, .25f));
                display_message(currUnit.unitName + "'s turn...");
                
                //skip dead unit's turns and account for dying of poison
                if (currUnit.dead) 
                {
                    currUnit = null;
                }
                break;
            
            case TurnState.MENU:
                //Enemy selects action automatically
                if (state == BattleState.ENEMYTURN)
                {
                    //select a random option
                    int rand = Random.Range(0,2);
                    //decide between attacking and using an item
                    if (rand == 0)
                    {
                        OnAttackButton();
                    }
                    else
                    {
                        inv.passAction(currUnit.item_held);
                    }
                }

                //stall gameplay until an action is submitted
                if (turn_action != null)
                {
                    //include automatic generation of turn by enemies

                    //reset target then move to target phase of turn
                    turn_target = null;

                    //check for mixer using items
                    if (currUnit == turns[1])
                    {
                        // if he didn't attack, then it's an item
                        if (turn_action != currUnit.GenerateAttack())
                        {
                            StartCoroutine(DelayTurn(TurnState.MIX, 0f));
                            display_message("Choose another action to mix with " + turn_action.name);
                            mix_action = turn_action;
                            turn_action = null;
                            break;
                        }
                    }

                    //transition to next phase of turn
                    StartCoroutine(DelayTurn(TurnState.TARGET, 0f));

                    display_message("Click a target for " + currUnit.unitName + "'s " + turn_action.name + "...");
                }
                break;

            case TurnState.MIX:
                //stall gameplay until an action is submitted
                if (turn_action != null)
                {
                    //reset target then move to target phase of turn
                    turn_target = null;

                    //mix the previos item and the new item
                    turn_action = new action("item mix", turn_action, mix_action);

                    //transition to next phase of turn
                    StartCoroutine(DelayTurn(TurnState.TARGET, 0f));

                    display_message("Click a target for " + currUnit.unitName + "'s " + turn_action.name + "...");
                }
                break;

            case TurnState.TARGET:
                //Enemy selects action automatically
                if (state == BattleState.ENEMYTURN)
                {
                    //select a random space
                    int rand = Random.Range(0,3);
                    //switch to ally side if healing
                    if (turn_action.hurt != true) {rand += 3;}
                    //choose target
                    turn_target = turns[rand];
                }

                //stall gameplay until a target is submitted
                if (turn_target != null)
                {
                    turn_state = TurnState.ACTION;
                }
                break;

            case TurnState.ACTION:
                //perform the action on the target
                turn_target.TakeAction(turn_action);
                if (turn_action.hurt) {display_message(currUnit.unitName + "'s " + turn_action.name + " dealt " + turn_action.damage + " damage to " + turn_target.unitName);}
                else {display_message(currUnit.unitName + "'s " + turn_action.name + " restored " + turn_action.damage + " health to " + turn_target.unitName + "!");}
                //transition to next phase of turn
                StartCoroutine(DelayTurn(TurnState.END, 1f));
                break;

            case TurnState.END:
                //take bleed damage after your turn
                if (currUnit.bleed != 0)
                {
                    display_message( currUnit.unitName + " is bleeding out!");
                    currUnit.TakeAction( new action("Bleed Damage", 10*currUnit.bleed, true) );
                }
                //end turn
                currUnit = null;

                //check for the end of the battle
                checkEnd();

                break;
            
            case TurnState.FINISH:
                //return to map or menu
                if(state == BattleState.WON)
                    {
                        SceneManager.LoadScene(1);
                    }
                else if(state == BattleState.LOST)
                    {
                        DeleteSaveFileBattle.deleteData();
                        SceneManager.LoadScene(0);
                    }
                break;
            }
        }
    }


    void display_message(string input = "Battle in progess...")
    {
        dialogueText.text = input;
    }
}
