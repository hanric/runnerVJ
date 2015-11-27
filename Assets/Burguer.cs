using UnityEngine;
using System.Collections;

public class Burguer : MonoBehaviour {

	public float degreesY = 3.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,degreesY,0,Space.World);
	}
}
