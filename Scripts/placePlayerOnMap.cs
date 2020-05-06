using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placePlayerOnMap : MonoBehaviour
{
    public GameObject playerprefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerGO = Instantiate(playerprefab);
    }

}
