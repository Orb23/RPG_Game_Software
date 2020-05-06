using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class grid_move : MonoBehaviour
{

    public float speed;


    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        LoadPlayer();
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb2d.AddForce(movement * speed);
        //UpdatePosition();

    }

    public void SavePlayer()
    {
        SaveMapSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveMapSystem.LoadPlayer();
        if(data == null)
        {
            //do nothing, file is not found to update player position...
            // OR the player has entered the map scene for the first time.
            // hence, they should not have a playerdata.data file.
        }
        else
        {
            Vector3 position;

            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];


            transform.position = position;
        }

    }

    //used only in testing purposes, commented out in FixedUpdate()
    public void UpdatePosition()
    {
        Debug.Log(transform.position.x + ", " + transform.position.y);

    }


    //whenever a scene is switches, everything is destroyed, thus
    // the last position will be saved before switching to the new scene.
     void OnDestroy()
    {
        SavePlayer();
    }

}
