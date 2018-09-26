using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    // Paddle Speed
    public float speed = 7.0f;
    // Keyboard or controller
    public int playerIndex = 1;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
         // Sets keyboard or controller
        float verticalMovement = Input.GetAxis ("Vertical" + playerIndex);

        // Moves paddle vertically
        GetComponent<Rigidbody>().velocity = new Vector3 (
            0f, 
            verticalMovement * speed, 
            0f
        );
	}
   
}
