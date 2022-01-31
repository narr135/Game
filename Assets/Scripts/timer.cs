using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class timer : MonoBehaviour
{
    public float timeLeft = 4f;
    public bool timerActive = false;
    public bool gameEnded = false;
    public TextMeshProUGUI timerText;
    public GameObject gameOverScreen;
    public GameObject gameStartScreen;
    public GameObject player1;
    public GameObject player2;
    public GameObject gameName;
    public GameObject startInstructions;
    public GameObject restartInstructions;

    private void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        gameName = GameObject.Find("GAME NAME");
        gameOverScreen = GameObject.Find("Game Over");
        gameStartScreen = GameObject.Find("Game Start");
        startInstructions = GameObject.Find("Start Instructions");
        restartInstructions = GameObject.Find("Restart Instructions");
        player1.GetComponent<Player1Movement>().enabled = false;
        player2.GetComponent<Player2Movement>().enabled = false;
        gameOverScreen.SetActive(false);
        gameStartScreen.SetActive(true);
        restartInstructions.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            timerStart();
        }
        if (gameEnded == true)
        {
            gameOver();
        }
    }

    public void gameStart()
    {
        gameEnded = false;
        gameStartScreen.SetActive(false);
        startInstructions.SetActive(true);
        gameName.SetActive(true);
        player1.GetComponent<Player1Movement>().enabled = true;
        player2.GetComponent<Player2Movement>().enabled = true;
    }

    public void gameRestart()
    {
        gameEnded = false;
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void timerStart()
    {
        timerActive = true;
        if (timerActive == true)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = (timeLeft).ToString("0");
            restartInstructions.SetActive(false);
            startInstructions.SetActive(false);
            gameName.SetActive(false);
            if (timeLeft < 1)
            {
                timerActive = false;
                timeLeft = 4f;
                gameStart();
            }
            else if (!Input.GetButton("Fire1") && !Input.GetButton("Fire2"))
            {
                restartInstructions.SetActive(true);
                timerText.enabled = false;
                timerActive = false;
                timeLeft = 4f;
            }
        }
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        player1.GetComponent<Player1Movement>().enabled = false;
        player2.GetComponent<Player2Movement>().enabled = false;
        if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            gameStartScreen.SetActive(false);
            player1.GetComponent<Player1Movement>().enabled = true;
            player2.GetComponent<Player2Movement>().enabled = true;
            gameRestart();
            gameEnded = false;
        }
    }
}