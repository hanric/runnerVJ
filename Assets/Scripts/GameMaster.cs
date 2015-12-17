using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
	
	private static bool isGameOver;
	public static bool isPaused;
	public static bool hasEnded;

	public Image StaminaBar;
	float originalStaminaWidth;
	float originalStaminaX;

	public Text burguerText;
	public static int burguersCollected;

	public Text donutText;
	public static int donutsCollected;

	public Text finalPointsText;
	int finalPoints;

	int pointsPerDonut = 10;
	int pointsPerBurguer = 50;
	int pointsPerStamina = 10;

	// Use this for initialization
	void Start () {
		isGameOver = false;
		originalStaminaWidth = StaminaBar.rectTransform.sizeDelta.x;
		originalStaminaX = StaminaBar.rectTransform.anchoredPosition.x;

		burguersCollected = 0;
		burguerText.text = "x0";

		donutsCollected = 0;
		donutText.text = "x0";

		finalPoints = 0;
		finalPointsText.enabled = false;

		//StartCoroutine (DrawEndGraphics());
	}
	
	// Update is called once per frame
	void Update () {

		if (!isPaused) {
			//staminaText.text = "Stamina: " + ((int) Player.stamina).ToString ();
			StaminaBar.rectTransform.sizeDelta = new Vector2 ((Player.stamina * originalStaminaWidth) / 100.0f, StaminaBar.rectTransform.sizeDelta.y);
			StaminaBar.rectTransform.anchoredPosition = new Vector2 (originalStaminaX, StaminaBar.rectTransform.anchoredPosition.y);
		}

		burguerText.text = "x" + burguersCollected.ToString ();
		donutText.text = "x" + donutsCollected.ToString ();

		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (!isPaused) pause();
			else unpause();
		}

		if (isGameOver && Time.timeScale > 0) {
			//if (Time.timeScale > 0.1) Time.timeScale -= 0.001f;
			//else Time.timeScale = 0;
		}

		if (hasEnded) {
			pause ();
			endLevel ();
		}
	}

	public static void gameOver() {
		isGameOver = true;
		Player player = (Player)FindObjectOfType (typeof(Player));
		player.die ();
		//Time.timeScale = 0.3f;
	}

	public static bool getGameOver() {
		return isGameOver;
	}

	void pause() {
		isPaused = true;
		Time.timeScale = 0.0f;
	}

	public static void unpause() {
		isPaused = false;
		Time.timeScale = 1.0f;
	}

	void endLevel() {
		DrawEndGraphics ();
		string currentLevel = Application.loadedLevelName;
		if (currentLevel.Equals("scene1")) {
			Application.LoadLevel ("scene2");
		} else if (currentLevel.Equals("scene2")) {
			Application.LoadLevel ("menu");
		}
	}

	public void DrawEndGraphics() {
		//yield return new WaitForSeconds (0.5f);
		//while (true) {
			if (hasEnded) {
				finalPointsText.enabled = true;
				// show big donut

				// count donuts
				int donutsCollectedAux = donutsCollected;
				for (int i=0; i<donutsCollectedAux; ++i) {
					//yield return new WaitForSeconds (0.5f);
					--donutsCollected;
					donutText.text = "x" + donutsCollected.ToString ();

					//yield return new WaitForSeconds (0.5f);
					for (int j=0; j<pointsPerDonut; ++j) {
						//yield return new WaitForSeconds (0.01f);
						++finalPoints;
						finalPointsText.text = finalPoints.ToString () + "points";
					}
				}

				//hide big donut

				//yield return new WaitForSeconds (0.5f);
				//show big burguer

				//yield return new WaitForSeconds (0.5f);
				// count burguers
				int burguersCollectedAux = burguersCollected;
				for (int i=0; i<burguersCollectedAux; ++i) {
					//yield return new WaitForSeconds (0.5f);
					--burguersCollected;
					burguerText.text = "x" + burguersCollected.ToString ();
			
					//yield return new WaitForSeconds (0.5f);
					for (int j=0; j<pointsPerBurguer; ++j) {
						//yield return new WaitForSeconds (0.01f);
						++finalPoints;
						finalPointsText.text = finalPoints.ToString () + "points";
					}
				}

				//yield return new WaitForSeconds (0.5f);

				//count stamina
				int staminaAux = (int)Player.stamina;
				for (int i=0; i< staminaAux; ++i) {
					Player.stamina -= 1.0f;
					StaminaBar.rectTransform.sizeDelta = new Vector2 ((Player.stamina * originalStaminaWidth) / 100.0f, StaminaBar.rectTransform.sizeDelta.y);
					StaminaBar.rectTransform.anchoredPosition = new Vector2 (originalStaminaX, StaminaBar.rectTransform.anchoredPosition.y);

					for (int j=0; j<pointsPerStamina; ++j) {
						//yield return new WaitForSeconds (0.01f);
						++finalPoints;
						finalPointsText.text = finalPoints.ToString () + "points";
					}
				}
				//yield return null;
			}
		}
	//}
}
