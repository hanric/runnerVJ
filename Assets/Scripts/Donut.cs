using UnityEngine;
using System.Collections;

public class Donut : MonoBehaviour {

	public float degreesY = 3.0f;
	public float staminaPlus = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,degreesY,0,Space.World);
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals("Player")) {
			other.GetComponent<Player>().updateStamina(staminaPlus, "Donut");
			Destroy(gameObject);
			++GameMaster.burguersCollected;
		}
	}
}
