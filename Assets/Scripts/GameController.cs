﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject ballPrefab;
    public Text score1Text;
    public Text score2Text;
    public float scoreCoordinates = 3.4f;

    private Ball currentBall;
    private int score1 = 0;
    private int score2 = 0;

    // Use this for initialization
    void Start () {
        SpawnBall ();
    }

    void SpawnBall () {
        GameObject ballInstance = Instantiate(ballPrefab, transform);

        currentBall = ballInstance.GetComponent<Ball>();
        currentBall.transform.position = Vector3.zero;

        score1Text.text = "P1: " + score1.ToString();
        score2Text.text = "P2: " + score2.ToString();
    }

	
	// Update is called once per frame
	void Update () {
		if (currentBall != null) {
            if (currentBall.transform.position.x > scoreCoordinates) {
                score1++;
                Destroy(currentBall.gameObject);
                SpawnBall();
            }

            if (currentBall.transform.position.x < -scoreCoordinates) {
                score2++;
                Destroy(currentBall.gameObject);
                SpawnBall();
            }
        }
	}
}
