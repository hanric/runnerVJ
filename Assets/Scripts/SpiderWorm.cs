using UnityEngine;
using System.Collections;

public class SpiderWorm : MonoBehaviour {

	Animation anim;
	SphereCollider sc;
	BoxCollider bc;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animation> ();
		sc = gameObject.GetComponent<SphereCollider> ();
		bc = gameObject.GetComponent<BoxCollider> ();
		sc.tag = "sphereCollider";
		bc.tag = "boxCollider";
	}
	
	// Update is called once per frame
	void Update () {

	}

	//MANAGE COLLISIONS
	void OnCollisionEnter(Collision collision) {
		if (collision.contacts [0].thisCollider == sc) {
			if (collision.gameObject.name.Equals ("Player")) {
				Debug.Log ("ASLFKJASÑLDGKJAÑLDGSKJA");
				anim.PlayQueued ("Attack", QueueMode.PlayNow);
				anim.PlayQueued ("Idle", QueueMode.CompleteOthers);	
			}
			Physics.IgnoreCollision (collision.collider, sc);
		} else {
			Physics.IgnoreCollision (collision.collider, bc);
		}
	}
}
