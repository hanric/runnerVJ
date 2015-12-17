using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	public static float speed = 0.05f;
	Animator animator;

	int WALKING_SOUND_LENGTH = 15;
	int ATTACKING_SOUND_LENGTH = 4;

	AudioClip[] clipsWalking;
	AudioClip[] clipsAttacking;

	AudioSource audioWalk;
	AudioSource audioAttack;

	// Use this for initialization
	void Start () {

		animator = (Animator) GetComponent("Animator");
		// Workaround, the speed of the animation is randomly changed for 0.1 seconds so it seems like they start at different moments
		animator.speed = Random.Range(0,2000);
		StartCoroutine(wait ());
		animator.speed = 1;

		initSound ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameMaster.isPaused) {
			updatePosition ();
			updateSound ();
		}
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
			int randomSound = (int)Random.Range(0, ATTACKING_SOUND_LENGTH-1);
			audioAttack.clip = clipsAttacking[randomSound];
			audioAttack.Play();
			if (gameObject.name.Equals("BackZombie")) {	//zombie from the back (game over)
				GameMaster.gameOver();
			} else {
				Physics.IgnoreCollision(collision.collider , GetComponent<Collider>());
			}
		}
	}

	//sound
	void initSound() {
		clipsWalking = new AudioClip[WALKING_SOUND_LENGTH];
		for (int i = 1; i <= WALKING_SOUND_LENGTH; ++i) {
			clipsWalking[i-1] = (AudioClip)Resources.Load ("sounds/zombieWalking"+i, typeof(AudioClip));
		}
		clipsAttacking = new AudioClip[ATTACKING_SOUND_LENGTH];
		for (int i = 1; i <= ATTACKING_SOUND_LENGTH; ++i) {
			clipsAttacking[i-1] = (AudioClip)Resources.Load ("sounds/zombieAttack"+i, typeof(AudioClip));
		}

		audioWalk = gameObject.AddComponent<AudioSource> ();
		audioWalk.spatialBlend = 1.0f;
		audioWalk.volume = 0.6f;

		audioAttack = gameObject.AddComponent<AudioSource> ();
	}

	void updateSound() {
		if (!audioWalk.isPlaying) {
			float randomSeconds = Random.Range(0, 8);
			Invoke ("changeClip", randomSeconds);
		}
	}

	void changeClip() {
		int randomSound = (int)Random.Range(0, WALKING_SOUND_LENGTH-1);
		audioWalk.clip = clipsWalking[randomSound];
		audioWalk.Play();
	}
}
