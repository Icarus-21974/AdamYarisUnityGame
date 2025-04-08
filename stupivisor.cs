using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stupivisor : MonoBehaviour
{
    //these floats will be used as timers, that will be using fix update so there won't be a worry about the varible of what frame rate you get per machine
    public float powerUpTimer;
    public float gameTimer;

    //these are gonna be obects that will be called upon and the player will be interacting with
    public GameObject powerUpJump;
    public GameObject powerUpForce;
    public GameObject PlayerOne;
    public GameObject PlayerTwo;
    public GameObject pauseScreen;
    public GameObject unPauseButton;

    //booleans that will supply functions and will have different purposes in other classes
    public bool gameOver;
    public bool goMain;
    public bool gamePaused;
    public static bool timeOut;

    //these will be used for text boxes that the player will read or that will be used in different classes
    //Text static is a static variable so that when the scene changes that it can still be read by other classes 
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameText;
    public static string textStatic;

    //this is a list that will be used for spawning the locatiosn
    public List<Vector3> powerUpLocations;
    public List<int> randomNum;
    public int gen;
    public Vector3 spawn;



    // Start is called before the first frame update
    void Start()
    {
        //these are gonna be locations for random spawning of the power ups 
        powerUpLocations = new List<Vector3>();
        powerUpLocations.Add(new Vector3(0, 0, 0));
        powerUpLocations.Add(new Vector3(3, 4, 0));
        powerUpLocations.Add(new Vector3(-5, 7,0));
        powerUpLocations.Add(new Vector3(-2 ,7,0));
        powerUpLocations.Add(new Vector3(0 ,7,0));
        powerUpLocations.Add(new Vector3(-1, 7, 0));
        powerUpLocations.Add(new Vector3(0,7,0));
        powerUpLocations.Add(new Vector3(1, 7,0));
        powerUpLocations.Add(new Vector3(2, 7,0));
        powerUpLocations.Add(new Vector3(5, 7,0));

        randomNum = new List<int>();
        randomNum.Add(0);
        randomNum.Add(1);

        //intializing our variables
        powerUpTimer = 0;
        gameOver = false;
        gameTimer = 540;
        goMain = false;
        timeOut = false;
        gen = 0;

        pauseScreen.SetActive(false);
        unPauseButton.SetActive(false);
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //the meaining of "F2" is actually the decimal placing that is gonna be shown on screen
        //So we will only go 2 decimal places when game timer is being shown on screen
        timerText.text = gameTimer.ToString("F2");
        gameDone();
        randomPowerSpawn(powerUpLocations, randomNum);

        if (gameOver == true)
        {
            goMain = true;
        }
        
        if (goMain == true)
        {
            changeScene("MainMenu");
        }

        textStatic = gameText.text;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = true;
            pauseScreen.SetActive(true);
            unPauseButton.SetActive(true);
        }
    }

    //fixed update isn't depenedent on frames, and uses the start time of the scene/when the game starts.
    //This is better for timers since you don't have to worry about frames
    private void FixedUpdate()
    {
        if (gamePaused == true)
        {
            powerUpTimer = powerUpTimer;
            gameTimer = gameTimer;
        }
        else
        {
            powerUpTimer += Time.fixedDeltaTime;

            gameTimer -= Time.fixedDeltaTime;
        }

    }

    //this i sa funciton that will give us text based on the ending of our game 
    //like if player one or player two falls off the map or if there is a draw (time runs out)
    public void gameDone()
    {

        if(FindObjectOfType<PlayerScript>().p1FellOff == true)
        {
            timerText.text = "";
            gameText.text = "GAME OVER" + "\n" + "Player One Loses";
            gameOver = true;
        }

        if(FindObjectOfType<playerTwoScript>().p2FellOff == true)
        {
            timerText.text = "";
            gameText.text = "GAME OVER" + "\n" + "Player Two Loses";
            gameOver = true;
        }


        if(gameTimer <=0.1)
        {
            timerText.text = "";
            gameText.text = "Game Over" + "\n" + "DRAW";
            gameOver = true;
            timeOut = true;
        }
    }

    public void changeScene(string scenename)
    {
        SceneManager.GetSceneByName(scenename);
        SceneManager.LoadScene(scenename);
    }

    void randomPowerSpawn(List<Vector3> p, List<int> n)
    {
        if (powerUpTimer > 15)
        {
            spawn = p[Random.Range(0, p.Count)];
            gen += n[Random.Range(0, n.Count)];
            if (gen >0)
            {
                Instantiate(powerUpJump, spawn, Quaternion.identity);
            }
            else
            {
                Instantiate(powerUpForce, spawn, Quaternion.identity);
            }

            gen = 0;
            powerUpTimer = 0;
        }
    }

    public void unpauseGame()
    {
        gamePaused = false;
        pauseScreen.SetActive(false);
        unPauseButton.SetActive(false);
    }
}

