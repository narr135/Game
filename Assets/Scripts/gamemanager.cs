using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gamemanager : MonoBehaviour
{

    bool gameEnded = false;
    public GameObject gameOverScreen;
    public GameObject gameStartScreen;
    public GameObject player1;
    public GameObject player2;

    private void Start()
    {
        gameOverScreen = GameObject.Find("Game Over");
        gameStartScreen = GameObject.Find("Game Start");
        gameOverScreen.SetActive(false);
        gameStartScreen.SetActive(true);
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player1.GetComponent<Player1Movement>().enabled = false;
        player2.GetComponent<Player2Movement>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            gameStart();
        }
    }

    public void gameStart()
    {
        gameEnded = false;
        gameStartScreen.SetActive(false);
        player1.GetComponent<Player1Movement>().enabled = true;
        player2.GetComponent<Player2Movement>().enabled = true;
    }

    public void gameOver()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            gameOverScreen.SetActive(true);
            player1.GetComponent<Player1Movement>().enabled = false;
            player2.GetComponent<Player2Movement>().enabled = false;
            if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
            {
                gameStartScreen.SetActive(false);
                player1.GetComponent<Player1Movement>().enabled = true;
                player2.GetComponent<Player2Movement>().enabled = true;
                gameRestart();
            }
        }
    }

    public void gameRestart()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
