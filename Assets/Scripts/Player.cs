using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static float stamina = 80.0f;
	public float normalStaminaDecrease = -0.05f;
	public float normalStaminaIncrease = 0.1f;
	public bool isStaminaIncreasing;

	private bool isChangingSide = false;
	public static bool isJumping = false;

	public int jumpHeight = 9;
	public float speed = 0.1f;

	private float totalOffset = 0.0f;
	private float movementOffset = 0.0f;

	private float positionLeft = 0.0f;
	private float positionRight = 0.0f;
	private float positionCenter = 0.0f;

	//camera
	private Camera mainCamera;
	private Camera backCamera;

	/*
		0 LEFT
		1 CENTER
		2 RIGHT
	*/
	int positionState;
	/*	
	 	0 RUN 
		1 JUMP
	*/
	int animationState;

	Animator animator;
	GameObject plane;

	void Start () {
		animator = (Animator) GetComponent ("Animator");
		plane = GameObject.Find ("Plane");
		positionState = 1;
		animationState = 0;

		initCameras();

		initPositions ();

		isStaminaIncreasing = false;
		//gameObject.GetComponent<ParticleSystem> ().Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		updateStamina (0, "Normal");
		manageCamera();
		updatePosition();
		updateAnimation();
	}

	void initPositions() {
		// Movement offset for every "frame"
		movementOffset = plane.GetComponent<Collider>().bounds.size.x / (3*30.0f);
		GetComponent<Rigidbody> ().position = new Vector3 (plane.transform.position.x, GetComponent<Rigidbody> ().position.y, GetComponent<Rigidbody> ().position.z);
		
		// Initializing possible X's of the player
		positionLeft = plane.transform.position.x - plane.GetComponent<Collider>().bounds.size.x / 3;
		positionRight = plane.transform.position.x + plane.GetComponent<Collider>().bounds.size.x / 3;
		positionCenter = plane.transform.position.x;
		
		// Initializing state of the player
		positionState = 1;
	}

	void updatePosition() {
		updateZ();
		checkJump ();
		updateX();
	}

	void checkJump() {
		if (!isChangingSide && !isJumping) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				Invoke("updateY",0.4f);
				animationState = 1; // jump
				isJumping = true;
			}
		}
	}

	void updateZ() {
		float realSpeed = speed;
		if (isJumping && speed < Zombie.speed)
			realSpeed = Zombie.speed;
		GetComponent<Rigidbody> ().position += new Vector3 (0, 0, realSpeed);
	}

	void updateY() {
			GetComponent<Rigidbody>().velocity += new Vector3 (0,jumpHeight,0);
	}

	void updateX() {
		if (!isChangingSide) {
			if (Input.GetKeyDown(KeyCode.A) && positionState != 0) {
				--positionState;
				totalOffset = -plane.GetComponent<Collider>().bounds.size.x / 3.0f;
				isChangingSide = true;
			} else if (Input.GetKeyDown(KeyCode.D) && positionState != 2) {
				++positionState;
				totalOffset = plane.GetComponent<Collider>().bounds.size.x / 3.0f;
				isChangingSide = true;
			}
		} else {
			if (totalOffset < 0.0f) { // move left <-
				GetComponent<Rigidbody>().position -= new Vector3(movementOffset, 0, 0);
				totalOffset += movementOffset;
				if (totalOffset > 0.0f) {
					switch (positionState) {
					case 1 : 
						GetComponent<Rigidbody>().position = new Vector3(positionCenter, GetComponent<Rigidbody>().position.y, GetComponent<Rigidbody>().position.z);
						break;
					case 0 : 
						GetComponent<Rigidbody>().position = new Vector3(positionLeft, GetComponent<Rigidbody>().position.y, GetComponent<Rigidbody>().position.z);
						break;
					}
					isChangingSide = false;
				}
			} else if (totalOffset > 0.0f) { // move right ->
				GetComponent<Rigidbody>().position += new Vector3(movementOffset, 0, 0);
				totalOffset -= movementOffset;
				if (totalOffset < 0.0f) {
					switch (positionState) {
					case 2 : 
						GetComponent<Rigidbody>().position = new Vector3(positionRight, GetComponent<Rigidbody>().position.y, GetComponent<Rigidbody>().position.z);
						break;
					case 1 : 
						GetComponent<Rigidbody>().position = new Vector3(positionCenter, GetComponent<Rigidbody>().position.y, GetComponent<Rigidbody>().position.z);
						break;
					}
					isChangingSide = false;
				}
			}	
		}
	}

	void updateAnimation() {
		if (animationState == 1) { // if jump
			animator.SetInteger ("state", animationState);
			animationState = 0;
		} else {
			animator.SetFloat ("stamina", stamina);
		}
	}

	//CAMERA MANAGEMENT
	void initCameras() {
		foreach (Camera c in Camera.allCameras) {
			if (c.gameObject.name == "Main Camera") {
				mainCamera = c;
				mainCamera.enabled = true;
			} else {
				backCamera = c;
				backCamera.enabled = false;
			}
		}
	}

	void manageCamera() {
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyUp(KeyCode.S)) {
			mainCamera.enabled = !mainCamera.enabled;
			backCamera.enabled = !backCamera.enabled;
		}
	}
	
	// STAMINA
	public void updateStamina(float qtt, string from) {
		stamina += qtt;
		if (from.Equals ("Burguer")) {
			//gameObject.GetComponent<ParticleSystem> ().Play ();
		} else if (from.Equals ("Normal")) {
			if (isStaminaIncreasing) {
				stamina += normalStaminaIncrease;
				if (stamina >= 80.0f) {
					isStaminaIncreasing = false;
				}
			} else {
				stamina += normalStaminaDecrease;
			}
		} else if (from.Equals ("Crash")) {
			stamina = 0.0f;
			isStaminaIncreasing = true;
		}
		if (stamina > 100.0f) {
			stamina = 100.0f;
		} else if (stamina <= 0.0f) {
			stamina = 0.0f;
		}
		speed = stamina / 1000.0f;
	}

	//MANAGE COLLISIONS
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name.Equals ("OldCar")) {
			Destroy (collision.gameObject.GetComponent<BoxCollider> ());
			updateStamina (0, "Crash");
		} else if (collision.gameObject.CompareTag ("Enemy")) {
			animator.Play("damageRecieved");
			updateStamina(-20, "Enemy");
		}
	}
}