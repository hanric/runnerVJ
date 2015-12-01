using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
	
	public Text staminaText;
	private static bool isGameOver;

	// Use this for initialization
	void Start () {
		isGameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		staminaText.text = "Stamina: " + ((int) Player.stamina).ToString ();
		if (isGameOver && Time.timeScale > 0) {
			if (Time.timeScale > 0.1) Time.timeScale -= 0.001f;
			else Time.timeScale = 0;
		}
	}

	public static void gameOver() {
		isGameOver = true;
		Time.timeScale = 0.2f;
	}

	public static bool getGameOver() {
		return isGameOver;
	}
}
