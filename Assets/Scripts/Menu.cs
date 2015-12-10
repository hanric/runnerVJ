﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public Button PlayB;

	public Button ControlsB;
	public Image ControlsInfoImage;
	bool areControlsShowing;

	public Button AboutB;
	public Image AboutInfoImage;
	bool isAboutShowing;

	void Start () {
		PlayB.onClick.AddListener (() => {
			PlayClick ();
		});

		ControlsB.onClick.AddListener (() => {
			ControlsClick (); 
		});
		areControlsShowing = false;
		ControlsInfoImage.gameObject.SetActive (false);

		AboutB.onClick.AddListener (() => {
			AboutClick (); 
		});
		isAboutShowing = false;
		AboutInfoImage.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void PlayClick() {
		Application.LoadLevel ("scene1");
	}

	void ControlsClick() {
		if (areControlsShowing) ControlsInfoImage.gameObject.SetActive (false);
		else ControlsInfoImage.gameObject.SetActive (true);
		areControlsShowing = !areControlsShowing;
	}

	void AboutClick() {
		if (isAboutShowing) AboutInfoImage.gameObject.SetActive (false);
		else AboutInfoImage.gameObject.SetActive (true);
		isAboutShowing = !isAboutShowing;
	}
}
