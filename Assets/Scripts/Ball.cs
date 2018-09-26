using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float difficultyMultiplier = 1.1f;

    public float minXSpeed = 0.8f;
    public float maxXSpeed = 1.2f;

    public float minYSpeed = 0.8f;
    public float maxYSpeed = 1.2f;
    
    private Rigidbody ballRigidbody;

	// Use this for initialization
	void Start () {
        ballRigidbody = GetComponent<Rigidbody> ();
        ballRigidbody.velocity = new Vector3(
            Random.Range(minXSpeed, maxXSpeed) * Random.value > 0.5f ? -1 : 1, 
            Random.Range(minYSpeed, maxYSpeed) * Random.value > 0.5f ? -1 : 1, 
            0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.tag == "Border") {

            //Collided with the top limit
            if (otherCollider.transform.position.y > transform.position.y && ballRigidbody.velocity.y > 0) {
                ballRigidbody.velocity = new Vector3(
                    ballRigidbody.velocity.x, 
                    -ballRigidbody.velocity.y, 
                    0f
                );
            }

            //Collided with the bottom limit
            if (otherCollider.transform.position.y < transform.position.y && ballRigidbody.velocity.y < 0) {
                ballRigidbody.velocity = new Vector3(
                    ballRigidbody.velocity.x, 
                    -ballRigidbody.velocity.y, 
                    0f
                );

            }

        } else if (otherCollider.tag == "Paddle") {
            //Collected with left paddle
            if (otherCollider.transform.position.x < transform.position.x && transform.position.x < 0) {
                ballRigidbody.velocity = new Vector3(
                    -ballRigidbody.velocity.x * difficultyMultiplier, 
                    ballRigidbody.velocity.y * difficultyMultiplier, 
                    0f
                );
                
            }

            //Collected with right paddle
            if (otherCollider.transform.position.x > transform.position.x && transform.position.x > 0) {
                ballRigidbody.velocity = new Vector3(
                    -ballRigidbody.velocity.x * difficultyMultiplier, 
                    ballRigidbody.velocity.y * difficultyMultiplier, 
                    0f
                );
                
            }
        }
    }
}
