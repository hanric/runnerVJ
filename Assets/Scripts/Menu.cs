using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public Button PlayB;

	void Start () {
		PlayB.onClick.AddListener (() => {
			PlayClick ();
		});

	}
	
	// Update is called once per frame
	void Update () {

	}

	public static void PlayClick() {
		Application.LoadLevel ("scene1");
	}
}
