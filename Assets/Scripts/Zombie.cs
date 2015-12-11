using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	public static float speed = 0.05f;
	Animator animator;

	// Use this for initialization
	void Start () {

		animator = (Animator) GetComponent("Animator");
		// Workaround, the speed of the animation is randomly changed for 0.1 seconds so it seems like they start at different moments
		animator.speed = Random.Range(0,2000);
		StartCoroutine(wait ());
		animator.speed = 1;
	}
	
	// Update is called once per frame
	void Update () {
		updatePosition ();
	}

	void updatePosition() {
		updateZ();
	}
	
	void updateZ() {
		if (gameObject.name.Equals ("BackZombie")) {
			GetComponent<Rigidbody> ().position += new Vector3 (0, 0, speed);
		} else {
			GetComponent<Rigidbody> ().position += new Vector3 (0, 0, -speed);
		}
	}

	IEnumerator wait() {
		yield return new WaitForSeconds (0.1f);
	}

	//MANAGE COLLISIONS
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name.Equals("Player")) {
			animator.Play("attack");
			if (gameObject.name.Equals("BackZombie")) {	//zombie from the back (game over)
				GameMaster.gameOver();
			} else {
				Physics.IgnoreCollision(collision.collider , GetComponent<Collider>());
			}
		}
	}
}
