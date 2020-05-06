using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unit_HUD : MonoBehaviour
{
    public Slider sliderHP;
    public Slider sliderPoison;
    public Slider sliderBleed;
    public Slider sliderPara;

    public Unit owner;

    RaycastHit hit;

    //find stuff
    Camera cam;

    public void Start()
    {

        // get camera and canvas
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (Physics.Raycast(new Ray(cam.ScreenToWorldPoint(transform.position), Vector3.down), out hit))
            owner = hit.collider.GetComponent<Unit>();


    }

    public void Update()
    {
        sliderHP.maxValue = owner.maxHP;
        sliderHP.value = owner.currentHP;

        sliderPoison.maxValue = owner.maxStatus;
        sliderPoison.value = owner.poison;

        sliderBleed.maxValue = owner.maxStatus;
        sliderBleed.value = owner.bleed;

        sliderPara.maxValue = owner.maxStatus;
        sliderPara.value = owner.paralyze;

    }
}