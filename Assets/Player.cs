using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private bool isChangingSide = false;
	public static bool isJumping = false;

	public int jumpHeight = 5;
	public float speed = 0.1f;

	private float totalOffset = 0.0f;
	private float movementOffset = 0.0f;

	private float positionLeft = 0.0f;
	private float positionRight = 0.0f;
	private float positionCenter = 0.0f;

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

		initPositions ();
	}
	
	// Update is called once per frame
	void Update () {
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
		updateY();
		updateX();
	}

	void updateZ() {
		GetComponent<Rigidbody> ().position += new Vector3 (0, 0, speed);
		//GetComponent<Rigidbody> ().position = new Vector3 (GetComponent<Rigidbody> ().position.x, GetComponent<Rigidbody> ().position.y, GetComponent<Rigidbody> ().position.z + speed);
	}

	void updateY() {
		if (!isChangingSide && !isJumping) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				GetComponent<Rigidbody>().velocity += new Vector3 (0,jumpHeight,0);
				//GetComponent<Rigidbody>().velocity.y = jumpHeight;
				animationState = 1; // jump
				isJumping = true;
			}
		}
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
			animator.SetInteger("state", animationState);
			animationState = 0;
		}
	}
}