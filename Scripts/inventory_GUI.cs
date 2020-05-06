using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_GUI : MonoBehaviour
{
    private Inventory inv;


    void Start()
    {

        inv = GameObject.FindObjectOfType(typeof(Inventory)) as Inventory;

    }

    public void OnOpenButtonClick()
    {
        inv.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnCloseButtonClick()
    {
        inv.transform.GetChild(0).gameObject.SetActive(false);
    }
}
