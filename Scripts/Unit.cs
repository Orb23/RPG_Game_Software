using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public Sprite idle_sprite;
    public Sprite death_picture;
    public Sprite attacking;
    public Sprite using_item;

    public int damage;

    public int maxHP;
    public int currentHP;

    public int item_held;
    public bool dead;

    public int poison;
    public int bleed;
    public int paralyze;

    //cap the amount of status that can be accumulated
    public int maxStatus = 3;

    private BattleSystem battleSystem;
    private Inventory inv;

    public GameObject HUD;

    //ui elements
    Camera cam;
    GameObject canvas;
    Transform spawn;


    void Start()
    {
        //create a collider for mouse clicks
        BoxCollider bc = gameObject.AddComponent<BoxCollider>() as BoxCollider;
        //find the battleSystem for the current battle, very cursed
        battleSystem = GameObject.FindObjectOfType(typeof(BattleSystem)) as BattleSystem;

        //get camera and canvas
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas = GameObject.Find("Battle_UI_Canvas");
        spawn = GetComponent<Transform>().transform;

        //HUD = Resources.Load("Prefab/BattleGUI Prefab/UnitHUD") as GameObject;
        (Instantiate(HUD, HUD.transform.position = cam.WorldToScreenPoint(spawn.position) + new Vector3(0,100,0), transform.rotation) as GameObject).transform.parent = canvas.transform;
    }

    void OnMouseDown()
    {
        //cursed
        //battleSystem = GameObject.FindObjectOfType(typeof(BattleSystem)) as BattleSystem;
        battleSystem.TargetSelect(this);
    }

    public void TakeAction(action a)
    {
        int damage_sign = 1;

        if (!a.hurt && a.recover && dead) {
            dead = false;
            this.GetComponent<SpriteRenderer>().sprite = idle_sprite;

        }

        if (a.hurt && a.recover)
        {
            inv = GameObject.FindObjectOfType(typeof(Inventory)) as Inventory;
            inv.incItem(item_held);
        }

        if (!dead)
        {
            if (a.hurt) { damage_sign = -1; }
            currentHP = Mathf.Clamp(currentHP + a.damage * damage_sign, 0, maxHP);
            poison = Mathf.Clamp(poison - a.poison * damage_sign, 0, 3);
            bleed = Mathf.Clamp(bleed - a.bleed * damage_sign, 0, 3);
            paralyze = Mathf.Clamp(paralyze - a.paralyze * damage_sign, 0, 3);

            //put item refund/item stealing before checking for death
            //if (a.hurt && a.recover) {add the item the enemy holds to the inventory}

            //check for 0 hp to set unit to dead
            if (currentHP <= 0) {
                dead = true;
                this.GetComponent<SpriteRenderer>().sprite = death_picture;

            }

            Debug.Log( unitName + " gained " + damage*damage_sign + " health and now has " + currentHP);
        }
    }

    //generate a default attack action and return it
    public action GenerateAttack()
    {
        return ( new action("attack", damage, true) );
    }


    // These two functions are currently being used to make the code run.
    // This section updates the values of the unit.
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

}
