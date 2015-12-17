using UnityEngine;
using System.Collections;

public class SpiderWorm : MonoBehaviour {

	Animation anim;
	SphereCollider sc;
	BoxCollider bc;
	
	int WALKING_SOUND_LENGTH = 0;
	int ATTACKING_SOUND_LENGTH = 3;
	
	AudioClip[] clipsWalking;	//standing presence sound (approaching will increment)
	AudioClip[] clipsAttacking;
	
	AudioSource audioWalk;
	AudioSource audioAttack;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animation> ();
		sc = gameObject.GetComponent<SphereCollider> ();
		bc = gameObject.GetComponent<BoxCollider> ();
		initSound ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	//MANAGE COLLISIONS
	void OnCollisionEnter(Collision collision) {
		if (collision.contacts [0].thisCollider == sc) {
			if (collision.gameObject.name.Equals ("Player")) {
				anim.PlayQueued ("Attack", QueueMode.PlayNow);
				anim.PlayQueued ("Idle", QueueMode.CompleteOthers);
			}
			Physics.IgnoreCollision (collision.collider, sc);
			int randomSound = (int)Random.Range(0, ATTACKING_SOUND_LENGTH-1);
			audioAttack.clip = clipsAttacking[randomSound];
			audioAttack.Play();
		} else {
			Physics.IgnoreCollision (collision.collider, bc);
		}
	}

	//sound
	void initSound() {
		clipsWalking = new AudioClip[WALKING_SOUND_LENGTH];
		for (int i = 1; i <= WALKING_SOUND_LENGTH; ++i) {
			clipsWalking[i-1] = (AudioClip)Resources.Load ("sounds/zombieWalking"+i, typeof(AudioClip));
		}

		audioWalk = gameObject.AddComponent<AudioSource> ();
		audioWalk.spatialBlend = 1.0f;
		audioWalk.volume = 0.6f;

		clipsAttacking = new AudioClip[ATTACKING_SOUND_LENGTH];
		for (int i = 1; i <= ATTACKING_SOUND_LENGTH; ++i) {
			clipsAttacking[i-1] = (AudioClip)Resources.Load ("sounds/caveWormAttack"+i, typeof(AudioClip));
		}
		
		audioAttack = gameObject.AddComponent<AudioSource> ();
	}
}
