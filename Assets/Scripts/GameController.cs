using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject ballPrefab;
    public GameObject aiControl;
    public GameObject controllerControl;

    public Text score1Text;
    public Text score2Text;
    public Text gameOver;
    public Text playAgain;
    public InputField command;
    public float scoreCoordinates = 3.4f;
    public int winningScore = 3;
    public Canvas console;

    private Color colour1 = new Color32(130, 215, 210, 255);
    private Color colour2 = Color.blue;

    private Ball currentBall;
    private int score1 = 0;
    private int score2 = 0;
    private bool over = false;
    private bool inputActive = false;
    private bool aiOn = false;
    private bool cmdOn = false;
    private bool controllerOn = false;

    // Use this for initialization
    void Start () {
        SpawnBall ();

        InputField input = command.GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update () {
        ActiveCommandPrompt ();

        CheckScore ();

        Rounds ();

        PlayAgain();
        AIToggle();
        ControllerToggle();
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
                console.enabled = true;
                inputActive = true;
                cmdOn = true;
            }
        } 

        if (Input.GetKeyUp(KeyCode.Return) && inputActive) {
            command.text = "";
        }

        if (cmdOn) {
            if (Input.GetKeyDown(KeyCode.A)) { command.text += "a"; }
            else if (Input.GetKeyDown(KeyCode.B)) { command.text += "b"; }
            else if (Input.GetKeyDown(KeyCode.C)) { command.text += "c"; }
            else if (Input.GetKeyDown(KeyCode.D)) { command.text += "d"; }
            else if (Input.GetKeyDown(KeyCode.E)) { command.text += "e"; }
            else if (Input.GetKeyDown(KeyCode.F)) { command.text += "f"; }
            else if (Input.GetKeyDown(KeyCode.G)) { command.text += "g"; }
            else if (Input.GetKeyDown(KeyCode.H)) { command.text += "h"; }
            else if (Input.GetKeyDown(KeyCode.I)) { command.text += "i"; }
            else if (Input.GetKeyDown(KeyCode.J)) { command.text += "j"; }
            else if (Input.GetKeyDown(KeyCode.K)) { command.text += "k"; }
            else if (Input.GetKeyDown(KeyCode.L)) { command.text += "l"; }
            else if (Input.GetKeyDown(KeyCode.M)) { command.text += "m"; }
            else if (Input.GetKeyDown(KeyCode.N)) { command.text += "n"; }
            else if (Input.GetKeyDown(KeyCode.O)) { command.text += "o"; }
            else if (Input.GetKeyDown(KeyCode.P)) { command.text += "p"; }
            else if (Input.GetKeyDown(KeyCode.Q)) { command.text += "q"; }
            else if (Input.GetKeyDown(KeyCode.R)) { command.text += "r"; }
            else if (Input.GetKeyDown(KeyCode.S)) { command.text += "s"; }
            else if (Input.GetKeyDown(KeyCode.T)) { command.text += "t"; }
            else if (Input.GetKeyDown(KeyCode.U)) { command.text += "u"; }
            else if (Input.GetKeyDown(KeyCode.V)) { command.text += "v"; }
            else if (Input.GetKeyDown(KeyCode.W)) { command.text += "w"; }
            else if (Input.GetKeyDown(KeyCode.X)) { command.text += "x"; }
            else if (Input.GetKeyDown(KeyCode.Y)) { command.text += "y"; }
            else if (Input.GetKeyDown(KeyCode.Z)) { command.text += "z"; }
            else if (Input.GetKeyDown(KeyCode.Alpha0)) { command.text += "0"; }
            else if (Input.GetKeyDown(KeyCode.Alpha1)) { command.text += "1"; }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { command.text += "2"; }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { command.text += "3"; }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) { command.text += "4"; }
            else if (Input.GetKeyDown(KeyCode.Alpha5)) { command.text += "5"; }
            else if (Input.GetKeyDown(KeyCode.Alpha6)) { command.text += "6"; }
            else if (Input.GetKeyDown(KeyCode.Alpha7)) { command.text += "7"; }
            else if (Input.GetKeyDown(KeyCode.Alpha8)) { command.text += "8"; }
            else if (Input.GetKeyDown(KeyCode.Alpha9)) { command.text += "9"; }
            else if (Input.GetKeyDown(KeyCode.Space)) { command.text += " "; }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (command.text.Length > 0)
                {
                    command.text = command.text.Substring(0, command.text.Length - 1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                    Submit(command.text);
            }
        }
    }

    void Submit(string cmd)
    {
        if (cmd == "ai") {
            aiOn = !aiOn;
        }

        if (cmd == "controller") {
            controllerOn = !controllerOn;
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

        if (cmd == "quit") {
            console.enabled = false;
            inputActive = false;
            cmdOn = false;
            Time.timeScale = 1;
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
        } else {
            aiControl.GetComponent<AI>().playerIndex = 2;
        }
    }


    void ControllerToggle()
    {
        if (controllerOn) {
            controllerControl.GetComponent<Paddle>().playerIndex = 4;
        } else {
            controllerControl.GetComponent<Paddle>().playerIndex = 1;
        }
    }
}
