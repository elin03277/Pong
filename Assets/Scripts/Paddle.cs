using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public float speed = 7.0f;
    public int playerIndex = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float verticalMovement = Input.GetAxis("Vertical" + playerIndex);

        GetComponent<Rigidbody>().velocity = new Vector3(
            0f, 
            verticalMovement * speed, 
            0f
        );
	}
   
}
