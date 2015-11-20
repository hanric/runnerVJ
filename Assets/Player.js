#pragma strict

var player:Hashtable = new Hashtable();

var isChangingSide : boolean = false;
var totalOffset : float = 0.0;
var movementOffset : float = 0.0;

var positionLeft : float = 0.0;
var positionRight : float = 0.0;
var positionCenter : float = 0.0;

var positionState : int = 1; // 0 LEFT 1 CENTER 2 RIGHT

var animator : Animator;
var plane : GameObject;

function Start () {
	plane = GameObject.Find("Plane");
	animator = GetComponent("Animator");
	initPositions();
}

function initPositions() {	
	// Movement offset for every "frame"
	movementOffset = plane.GetComponent.<Collider>().bounds.size.x / (3*30.0);
	GetComponent.<Rigidbody>().position.x = plane.transform.position.x;
	
	// Initializing possible X's of the player
	positionLeft = plane.transform.position.x - plane.GetComponent.<Collider>().bounds.size.x / 3;
	positionRight = plane.transform.position.x + plane.GetComponent.<Collider>().bounds.size.x / 3;
	positionCenter = plane.transform.position.x;
	
	// Initializing state of the player
	positionState = 1;
}

function Update () {
	moveControl();
	updateAnimation();
}

function moveControl() {
	var plane : GameObject;
	plane = GameObject.Find("Plane");
	
	// Z player movement
	GetComponent.<Rigidbody>().position.z += 0.1;
	
	// X player movement
	if (!isChangingSide) {
		if (Input.GetKeyDown(KeyCode.A) && positionState != 0) {
			--positionState;
			totalOffset = -plane.GetComponent.<Collider>().bounds.size.x / 3.0;
			isChangingSide = true;
		} else if (Input.GetKeyDown(KeyCode.D) && positionState != 2) {
			++positionState;
			totalOffset = plane.GetComponent.<Collider>().bounds.size.x / 3.0;
			isChangingSide = true;
		} 
	} else {
		if (totalOffset < 0.0) { // move left <-
			GetComponent.<Rigidbody>().position.x -= movementOffset;
			totalOffset += movementOffset;
			if (totalOffset > 0.0) {
				switch (positionState) {
					case 1 : 
						GetComponent.<Rigidbody>().position.x = positionCenter;
						break;
					case 0 : 
						GetComponent.<Rigidbody>().position.x = positionLeft;
						break;
				}
				isChangingSide = false;
			}
		} else if (totalOffset > 0.0) { // move right ->
			GetComponent.<Rigidbody>().position.x += movementOffset;
			totalOffset -= movementOffset;
			if (totalOffset < 0.0) {
				switch (positionState) {
					case 2 : 
						GetComponent.<Rigidbody>().position.x = positionRight;
						break;
					case 1 : 
						GetComponent.<Rigidbody>().position.x = positionCenter;
						break;
				}
				isChangingSide = false;
			}
		}	
	}
}

function updateAnimation() {
	/*if (Input.GetKeyDown(KeyCode.W)) {
		//int runState = animator.GetInteger("runState");
		if (runState == 3) runState = 1;
		else ++runState; 
		animator.SetInteger("runState",3);
	}*/
}