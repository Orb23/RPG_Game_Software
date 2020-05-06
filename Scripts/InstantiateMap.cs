using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MapState { START, PICKUP, ENEMY }

public class InstantiateMap : MonoBehaviour
{

    public MapState state;

    public GameObject partyPrefab;
    public GameObject enemyPrefab;

    public Transform room_1;
    public Transform room_2;
    public Transform room_3;
    public Transform room_4;

    public Text infoText;





    // Start is called before the first frame update
    void Start()
    {
        state = MapState.START;
        StartCoroutine(SetupMap());

    }


    IEnumerator SetupMap()
    {


        yield return new WaitForSeconds(2f);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
