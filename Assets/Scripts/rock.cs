using UnityEngine;
using System.Collections;

public class rock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//MANAGE COLLISIONS
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name.Equals("Player")) {
			Physics.IgnoreCollision(collision.collider , GetComponent<Collider>());
		}
	}
}
