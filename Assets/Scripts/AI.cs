﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    // Paddle speed
    public float speed = 7.0f;
    // Keyboard or AI
    public int playerIndex = 2;

    // Used to keep track of the ball
    private GameObject ball;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

        // Controls player 2 movement
        if (playerIndex == 2) {
            float verticalMovement = Input.GetAxis ("Vertical" + playerIndex);

            GetComponent<Rigidbody>().velocity = new Vector3(
                0f,
                verticalMovement * speed,
                0f
            );
        
        // Controls ai movement
        } else if (playerIndex == 3) {
            ball = GameObject.FindGameObjectWithTag("Ball");

            if (ball != null) {
                if (ball.transform.position.y < 1.475 && ball.transform.position.y > -1.475) {
                    Vector3 paddlePosition = transform.position;
                    paddlePosition.y = Mathf.Lerp(transform.position.y, ball.transform.position.y, speed * Time.deltaTime);
                    transform.position = paddlePosition;
                }

            }
        }
         
    }

}
