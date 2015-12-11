using UnityEngine;
using System.Collections;

public class SpiderWorm : MonoBehaviour {

	Animation anim;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animation> ();
		gameObject.GetComponent<SphereCollider> ().tag = "sphereCollider";
		gameObject.GetComponent<BoxCollider> ().tag = "boxCollider";
	}
	
	// Update is called once per frame
	void Update () {

	}

	//MANAGE COLLISIONS
	void OnCollisionEnter(Collision collision) {
		Debug.Log ("ASLFKJASÑLDGKJAÑLDGSKJA");
		if (collision.contacts[0].thisCollider.tag.Equals("sphereCollider")) {
			if (collision.gameObject.name.Equals("Player")) {

				anim.PlayQueued("Attack", QueueMode.PlayNow);
				anim.PlayQueued("Idle", QueueMode.CompleteOthers);	
			}
		}
		Physics.IgnoreCollision(collision.collider , GetComponent<Collider>());
	}
}
