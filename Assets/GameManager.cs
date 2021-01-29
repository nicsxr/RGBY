using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int p1Health;
    [HideInInspector]
    public int p2Health;

    public Image p1HealthBar;
    public Image p2HealthBar;

    private int p1Score;
    private int p2Score;

    private int p1AllDamage;
    private int p2Alldamage;
    private bool roundFinished;

    [HideInInspector]
    public float timeLeft;
    private int maxScore;
    public GameObject winPanel;
    public TextMeshProUGUI winnerText;

    [HideInInspector]
    public GameObject p1;
    [HideInInspector]
    public GameObject p2;

    // UI TEXTS
    public TextMeshProUGUI p1HealthText;
    public TextMeshProUGUI p2HealthText;
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;
    public TextMeshProUGUI timeLeftText;
    //pause menu
    public GameObject pausePanel;
    private bool isPaused;


    //spawn system
    public GameObject[] characterPrefabs;

    public Transform[] spawnPoint;

    // Particles
    public GameObject damageParticle;
    public GameObject explosionParticle;

    private AudioManager audioManager;

    private int Xbox_One_Controller = 0;
    private int PS4_Controller = 0;
    void Start()
    {
        Time.timeScale = 1;
        p1 = Instantiate(characterPrefabs[PlayerPrefs.GetInt("p1Char")], spawnPoint[0].position, Quaternion.identity);
        p2 = Instantiate(characterPrefabs[PlayerPrefs.GetInt("p2Char")], spawnPoint[1].position, Quaternion.identity);
        timeLeft = PlayerPrefs.GetFloat("roundLength");
        maxScore = PlayerPrefs.GetInt("maxScore");
        p1.tag = "Player 1";
        p2.tag = "Player 2";
        // ....... ai ra vtqva ar vici xd

        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            print(names[x].Length);
            if (names[x].Length == 19)
            {
                print("PS4 CONTROLLER IS CONNECTED");
                PS4_Controller = 1;
                Xbox_One_Controller = 0;
            }
            if (names[x].Length == 22)
            {
                print("XBOX ONE CONTROLLER IS CONNECTED");
                //set a controller bool to true
                PS4_Controller = 0;
                Xbox_One_Controller = 1;

            }
        }


        if (Xbox_One_Controller == 1)
        {
            p1.GetComponent<playerMovement>().jumpBut = KeyCode.JoystickButton2;
            p1.GetComponent<playerMovement>().crouchBut = KeyCode.JoystickButton3;
            p1.GetComponent<playerMovement>().axis = "Horizontal";
            p1.GetComponent<playerShooting>().shootBut = KeyCode.JoystickButton0;
            p1.GetComponent<Abilities>().abKey = KeyCode.JoystickButton1;
        }
        else if (PS4_Controller == 1)
        {
            //do something
        }
        else
        {
            p1.GetComponent<playerMovement>().jumpBut = KeyCode.W;
            p1.GetComponent<playerMovement>().crouchBut = KeyCode.S;
            p1.GetComponent<playerMovement>().axis = "Horizontal";
            p1.GetComponent<playerShooting>().shootBut = KeyCode.Space;
            p1.GetComponent<Abilities>().abKey = KeyCode.Q;
        }


        //p2
        p2.GetComponent<playerMovement>().jumpBut = KeyCode.UpArrow;
        p2.GetComponent<playerMovement>().crouchBut = KeyCode.DownArrow;
        p2.GetComponent<playerMovement>().axis = "Hroizontal2";
        p2.GetComponent<playerShooting>().shootBut = KeyCode.Keypad0;
        p2.GetComponent<Abilities>().abKey = KeyCode.Keypad1;

        p1Health = 100;
        p2Health = 100;
        p1AllDamage = 0;
        p2Alldamage = 0;
        roundFinished = false;
        isPaused = false;
        audioManager = FindObjectOfType<AudioManager>();
        p1Score = PlayerPrefs.GetInt("p1Score", p1Score);
        p2Score = PlayerPrefs.GetInt("p2Score", p2Score);
    }

    private void Update()
    {

        //health
        p1HealthText.SetText(p1Health.ToString());
        p2HealthText.SetText(p2Health.ToString());
        p1HealthBar.fillAmount = p1Health * 0.01f;
        p2HealthBar.fillAmount = p2Health * 0.01f;
        //score
        p1ScoreText.SetText("Score: " + PlayerPrefs.GetInt("p1Score"));
        p2ScoreText.SetText("Score: " + PlayerPrefs.GetInt("p2Score"));
        if (timeLeft > 0 && roundFinished == false)
        {
            timeLeftText.color = Color.white;
            timeLeft -= Time.deltaTime;
            timeLeftText.SetText(timeLeft.ToString("0"));
        }
        if (p1Score >= maxScore || p2Score >= maxScore)
        {
            if (p1Score >= maxScore)
            {
                winnerText.SetText("Player 1 won!");
                timeLeftText.SetText("Match Finished!");
            }
            else if (p2Score >= maxScore)
            {
                winnerText.SetText("Player 2 won!");
                timeLeftText.SetText("Match Finished!");
            }
        }
        else
        {
            if (timeLeft <= 0)
            {
                if (p1AllDamage > p2Alldamage && roundFinished == false)
                {
                    roundFinished = true;
                    p1Score++;
                    Debug.Log(p1Score);
                    timeLeftText.color = Color.red;
                    timeLeftText.SetText("Player 1 Wins!");
                    PlayerPrefs.SetInt("p1Score", p1Score);
                    PlayerPrefs.SetInt("p2Score", p2Score);
                    StartCoroutine(endRound());
                }
                if (p2Alldamage > p1AllDamage && roundFinished == false)
                {
                    roundFinished = true;
                    p2Score++;
                    timeLeftText.color = Color.green;
                    timeLeftText.SetText("Player 2 Wins!");
                    PlayerPrefs.SetInt("p1Score", p1Score);
                    PlayerPrefs.SetInt("p2Score", p2Score);
                    StartCoroutine(endRound());
                }
                if (p1AllDamage == p2Alldamage && roundFinished == false)
                {
                    roundFinished = true;
                    timeLeftText.SetText("DRAW!");
                    PlayerPrefs.SetInt("p1Score", p1Score);
                    PlayerPrefs.SetInt("p2Score", p2Score);
                    StartCoroutine(endRound());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused == false)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                isPaused = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            p1Score = 0;
            p2Score = 0;
            PlayerPrefs.SetInt("p1Score", 0);
            PlayerPrefs.SetInt("p2Score", 0);
        }
    }
    IEnumerator endRound()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void p1Damaged(int damage)
    {
        p2Alldamage += damage;
        p1Health -= damage;
        audioManager.GetDamaged();
        Instantiate(damageParticle, p1.transform.position, Quaternion.identity);
        if (roundFinished == false)
        {
            if (p1Health <= 0)
            {
                p2Score++;
                PlayerPrefs.SetInt("p2Score", p2Score);
                roundFinished = true;
                if (p1Score == maxScore || p2Score == maxScore)
                {
                    p1.GetComponent<playerHealth>().playerDie();
                    StartCoroutine(endGame());
                }
                else
                {
                    p1.GetComponent<playerHealth>().playerDie();
                    StartCoroutine(endRound());
                }
            }
        }
    }
    
    public void p2Damaged(int damage)
    {
        p1AllDamage += damage;
        p2Health -= damage;
        audioManager.GetDamaged();
        Instantiate(damageParticle, p2.transform.position, Quaternion.identity);
        if (roundFinished == false)
        {
            if (p2Health <= 0)
            {
                p1Score++;
                PlayerPrefs.SetInt("p1Score", p1Score);
                roundFinished = true;
                if (p1Score == maxScore || p2Score == maxScore)
                {
                    p2.GetComponent<playerHealth>().playerDie();
                    StartCoroutine(endGame());
                }
                else
                {
                    p2.GetComponent<playerHealth>().playerDie();
                    StartCoroutine(endRound());
                }
            }
        }
    }

    public void spawnExplosion(Vector3 pos){
        Instantiate(explosionParticle, pos, Quaternion.identity);
    }

    public void healthPickup(int playerId)
    {
        if(playerId == 1)
        {
            p1Health += Random.Range(5, 25);
        }
        if(playerId == 2)
        {
            p2Health += Random.Range(5, 25);
        }
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }

    public void restartScene()
    {
        winPanel.SetActive(false);
        PlayerPrefs.DeleteKey("p1Score");
        PlayerPrefs.DeleteKey("p2Score");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // PAUSE MENU FUNCTIONS
    public void resumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void quitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.DeleteAll();
    }

}
