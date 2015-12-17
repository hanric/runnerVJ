using UnityEngine;
using System.Collections;

public class Donut : MonoBehaviour {

	public float degreesY = 3.0f;
	public float staminaPlus = 10.0f;

	//sound
	AudioClip donutPick;

	// Use this for initialization
	void Start () {
		donutPick = (AudioClip)Resources.Load ("sounds/donut", typeof(AudioClip));
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,degreesY,0,Space.World);
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals("Player")) {
			AudioSource.PlayClipAtPoint(donutPick, transform.position, 0.5f);
			other.GetComponent<Player>().updateStamina(staminaPlus, "Donut");
			Destroy(gameObject);
			++GameMaster.donutsCollected;
		}
	}
}
