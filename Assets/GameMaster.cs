using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
	
	public Text staminaText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		staminaText.text = "Stamina: " + ((int) Player.stamina).ToString ();
	}
}
