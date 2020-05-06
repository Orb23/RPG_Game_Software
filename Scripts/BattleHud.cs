using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    //public Slider poisonSlider;
    //private Unit owner;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        //poisonSlider.maxValue = unit.maxStatus;
        //poisonSlider.value = unit.poison;

        //owner = unit;

    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        //poisonSlider.value = owner.poison;
    }

    
}
