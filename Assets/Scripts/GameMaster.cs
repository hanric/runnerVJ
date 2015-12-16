﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
	
	private static bool isGameOver;

	public Image StaminaBar;
	float originalStaminaWidth;
	float originalStaminaX;

	public Text burguerText;
	public static int burguersCollected;

	public Text donutText;
	public static int donutsCollected;

	// Use this for initialization
	void Start () {
		isGameOver = false;
		originalStaminaWidth = StaminaBar.rectTransform.sizeDelta.x;
		originalStaminaX = StaminaBar.rectTransform.anchoredPosition.x;

		burguersCollected = 0;
		burguerText.text = "x0";

		donutsCollected = 0;
		donutText.text = "x0";
	}
	
	// Update is called once per frame
	void Update () {
		//staminaText.text = "Stamina: " + ((int) Player.stamina).ToString ();
		StaminaBar.rectTransform.sizeDelta = new Vector2 ((Player.stamina*originalStaminaWidth)/100.0f, StaminaBar.rectTransform.sizeDelta.y);
		StaminaBar.rectTransform.anchoredPosition = new Vector2 (originalStaminaX, StaminaBar.rectTransform.anchoredPosition.y);

		burguerText.text = "x" + burguersCollected.ToString ();
		donutText.text = "x" + donutsCollected.ToString ();

		if (isGameOver && Time.timeScale > 0) {
			//if (Time.timeScale > 0.1) Time.timeScale -= 0.001f;
			//else Time.timeScale = 0;
		}
	}

	public static void gameOver() {
		isGameOver = true;
		Time.timeScale = 0.3f;
	}

	public static bool getGameOver() {
		return isGameOver;
	}
}
