using UnityEngine;
using System.Collections;

public class Burguer : MonoBehaviour {

	public float degreesY = 3.0f;
	public int staminaPlus = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,degreesY,0,Space.World);
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals("Player")) {
			int staminaAux = GameMaster.stamina;
			staminaAux += staminaPlus;
			if (staminaAux > 100) GameMaster.stamina = 100;
			else GameMaster.stamina = staminaAux;
			Destroy(gameObject);
		}
	}
}
