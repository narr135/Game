using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{

    bool gameEnded = false;
    public GameObject gameOverScreen;


    private void Start()
    {
        gameOverScreen = GameObject.Find("Game Over");
        gameOverScreen.SetActive(false);
    }

    public void gameOver()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            gameOverScreen.SetActive(true);
            while (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Fire2"))
            {
                Invoke("gameRestart", 3f);
            }
        }
    }

    public void gameRestart()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
