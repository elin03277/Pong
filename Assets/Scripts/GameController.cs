using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject ballPrefab;
    public GameObject aiControl;
    public Text score1Text;
    public Text score2Text;
    public Text gameOver;
    public Text playAgain;
    public InputField command;
    public float scoreCoordinates = 3.4f;
    public int winningScore = 3;

    private Color colour1 = new Color32(130, 215, 210, 255);
    private Color colour2 = Color.blue;

    private Ball currentBall;
    private int score1 = 0;
    private int score2 = 0;
    private bool over = false;
    private bool inputActive = false;
    private bool aiOn = false;
    private bool cmdOn = false;

    // Use this for initialization
    void Start () {
        SpawnBall ();

        InputField input = command.GetComponent<InputField>();
        input.onEndEdit.AddListener(Submit);
    }

    // Update is called once per frame
    void Update () {
        ActiveCommandPrompt ();

        CheckScore ();

        Rounds ();

        PlayAgain();
        AIToggle();
    }

    void SpawnBall () {
        GameObject ballInstance = Instantiate(ballPrefab, transform);

        currentBall = ballInstance.GetComponent<Ball>();
        currentBall.transform.position = Vector3.zero;

        score1Text.text = "P1:" + score1.ToString();
        score2Text.text = "P2:" + score2.ToString();
    }

    void CheckScore () {
        if (score1 >= winningScore || score2 >= winningScore) {
            gameOver.enabled = true;
            playAgain.enabled = true;
            over = true;
        }
    }

    void ActiveCommandPrompt () {
        if (Input.GetKeyUp (KeyCode.C) && !cmdOn) {
            Time.timeScale = 0;
            if (inputActive == false) {
                command.enabled = true;
                command.text = "Cmd: \"ai, reset, colour\" Space to Exit";
                inputActive = true;
                cmdOn = true;
            }
        } else if (Input.GetKeyUp (KeyCode.Space) && cmdOn) {
            command.enabled = false;
            command.text = "";
            inputActive = false;
            cmdOn = false;
            Time.timeScale = 1;
        }

        if (cmdOn) {
            if (Input.GetKeyUp (KeyCode.Return)) {
                command.text = "Cmd: \"ai, reset, colour\" Space to Exit";
            }
        }
    }

    void Submit(string cmd)
    {
        if (cmd == "ai") {
            aiOn = !aiOn;
        }

        if (cmd == "colour") {
            if (Camera.main.backgroundColor == colour2) {
                Camera.main.backgroundColor = colour1;
            } else {
                Camera.main.backgroundColor = colour2;
            }
        }

        if (cmd == "reset") {
            score1Text.text = "P1:0";
            score2Text.text = "P2:0";
        }

    }

    void Rounds () {
        if (!over) {

            if (currentBall != null) {
                if (currentBall.transform.position.x > scoreCoordinates) {
                    score1++;
                    Destroy (currentBall.gameObject);
                    SpawnBall ();
                }

                if (currentBall.transform.position.x < -scoreCoordinates) {
                    score2++;
                    Destroy (currentBall.gameObject);
                    SpawnBall ();
                }
            }
        } else {
            if (currentBall != null) {
                Destroy (currentBall.gameObject);
            }
        }
    }

    void PlayAgain () {
        if (score1 >= winningScore || score2 >= winningScore) {
            if (Input.GetKeyUp (KeyCode.Space)) {
                score1 = 0;
                score2 = 0;
                gameOver.enabled = false;
                playAgain.enabled = false;
                over = false;
                SpawnBall ();
            }
        }
    }   

    void AIToggle () {
        if (aiOn) {
            aiControl.GetComponent<AI>().playerIndex = 3;
            aiControl.GetComponent<AI>().speed = 7;
        } else {
            aiControl.GetComponent<AI>().playerIndex = 2;
            aiControl.GetComponent<AI>().speed = 7;
        }
    }
    
}
