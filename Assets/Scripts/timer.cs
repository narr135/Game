﻿using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class timer : MonoBehaviour
{
	public float timeLeft = 3.49f;
	public bool timerActive;
	public bool gameEnded;
	public bool gameStarted;
	public bool gamePaused;
	public TextMeshProUGUI timerText;
	public Collider2D collider1;
	public Collider2D collider2;
	public GameObject gameOverScreen;
	public GameObject gameStartScreen;
	public GameObject pauseScreen;
	public GameObject player1;
	public GameObject player2;
	public GameObject gameName;
	public GameObject startInstructions;
	public GameObject restartInstructions;
	public Button continueButton;
	public Button restartButton;
	public Button exitButton;

	private void Start()
	{
		Time.timeScale = 1;
		gameEnded = false;
		gamePaused = false;
		timerActive = false;
		gameStarted = false;
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
		gameName = GameObject.Find("GAME NAME");
		pauseScreen = GameObject.Find("Pause Menu");
		gameOverScreen = GameObject.Find("Game Over");
		gameStartScreen = GameObject.Find("Game Start");
		startInstructions = GameObject.Find("Start Instructions");
		restartInstructions = GameObject.Find("Restart Instructions");
		collider1 = player1.GetComponent<BoxCollider2D>();
		collider2 = player2.GetComponent<BoxCollider2D>();
		player1.GetComponent<Player1Movement>().enabled = false;
		player2.GetComponent<Player2Movement>().enabled = false;
		pauseScreen.SetActive(false);
		gameOverScreen.SetActive(false);
		gameStartScreen.SetActive(true);
		restartInstructions.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetButtonDown("Cancel") && gamePaused == false)
		{
			gamePaused = true;
			Time.timeScale = 0;
			pauseScreen.SetActive(true);
			gameOverScreen.SetActive(false);
			gameStartScreen.SetActive(false);
			restartInstructions.SetActive(false);
			continueButton.onClick.AddListener(GameContinue);
			restartButton.onClick.AddListener(GameRestart);
			exitButton.onClick.AddListener(GameExit);
			player1.GetComponent<Player1Movement>().enabled = false;
			player2.GetComponent<Player2Movement>().enabled = false;
		}
		else if (Input.GetButtonDown("Cancel") && gamePaused == true)
		{
			gamePaused = false;
			GameContinue();
		}
		if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
		{
			TimerStart();
		}
		if (timerActive == true && !Input.GetButton("Fire1") && !Input.GetButton("Fire2"))
		{
			restartInstructions.SetActive(true);
			timerText.enabled = false;
			timerActive = false;
			timeLeft = 3.49f;
		}
		if (gameEnded == true)
		{
			GameOver();
		}
	}

	public void GameContinue()
	{
		if (gameEnded == true)
		{
			gameName.SetActive(false);
			pauseScreen.SetActive(false);
			gameOverScreen.SetActive(false);
			gameStartScreen.SetActive(false);
			startInstructions.SetActive(false);
			restartInstructions.SetActive(false);
			GameOver();
		}
		else if (gameStarted == false)
		{
			Time.timeScale = 1;
			gameName.SetActive(true);
			pauseScreen.SetActive(false);
			gameOverScreen.SetActive(false);
			gameStartScreen.SetActive(true);
			startInstructions.SetActive(true);
			restartInstructions.SetActive(false);
			player1.GetComponent<Player1Movement>().enabled = false;
			player2.GetComponent<Player2Movement>().enabled = false;
		}
		else
		{
			Time.timeScale = 1;
			gameName.SetActive(false);
			pauseScreen.SetActive(false);
			gameOverScreen.SetActive(false);
			gameStartScreen.SetActive(false);
			startInstructions.SetActive(false);
			restartInstructions.SetActive(false);
			player1.GetComponent<Player1Movement>().enabled = true;
			player2.GetComponent<Player2Movement>().enabled = true;
			TimerStart();
		}
	}

	public void GameExit()
	{
		Application.Quit();
	}

	public void GameStart()
	{
		Time.timeScale = 1;
		gameEnded = false;
		gameStarted = true;
		gameStartScreen.SetActive(false);
		startInstructions.SetActive(true);
		gameName.SetActive(true);
		player1.GetComponent<Player1Movement>().enabled = true;
		player2.GetComponent<Player2Movement>().enabled = true;
	}

	public void GameRestart()
	{
		gameEnded = false;
		gameStarted = false;
		gameOverScreen.SetActive(false);
		gameStartScreen.SetActive(false);
		pauseScreen.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void TimerStart()
	{
		Time.timeScale = 1;
		timerActive = true;
		if (timerActive == true)
		{
			timerText.enabled = true;
			timeLeft -= Time.deltaTime;
			timerText.text = (timeLeft).ToString("0");
			restartInstructions.SetActive(false);
			startInstructions.SetActive(false);
			gameName.SetActive(false);
			if (timeLeft < 1)
			{
				timerActive = false;
				timeLeft = 3.49f;
				if (gameStarted == false)
				{
					GameStart();
				}
				else { }
			}
		}
	}

	public void GameOver()
	{
		Time.timeScale = 1;
		gameOverScreen.SetActive(true);
		player1.GetComponent<Player1Movement>().enabled = false;
		player2.GetComponent<Player2Movement>().enabled = false;
		if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
		{
			gameStartScreen.SetActive(false);
			gameEnded = false;
			GameRestart();
		}
	}
}