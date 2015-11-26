#pragma strict

var player:Hashtable = new Hashtable();

var isChangingSide : boolean = false;
static var isJumping : boolean = false;

var jumpHeight = 5;
var speed = 0.1;

var totalOffset : float = 0.0;
var movementOffset : float = 0.0;

var positionLeft : float = 0.0;
var positionRight : float = 0.0;
var positionCenter : float = 0.0;

/*
	0 LEFT
	1 CENTER
	2 RIGHT
*/
var positionState : int;

/*	0 RUN 
	1 JUMP
*/
var animationState : int; 

var animator : Animator;
var plane : GameObject;

function Start () {
	plane = GameObject.Find("Plane");
	animator = GetComponent("Animator");
	
	positionState = 1;
	animationState = 0;
	
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
	updatePosition();
	updateAnimation();
}

function updatePosition() {
	updateZ();
	updateY();
	updateX();
}

function updateZ() {
	GetComponent.<Rigidbody>().position.z += speed;
}

function updateY() {
	if (!isChangingSide && !isJumping) {
		if (Input.GetKeyDown(KeyCode.Space)) {
			GetComponent.<Rigidbody>().velocity.y = jumpHeight;
			animationState = 1; // jump
			isJumping = true;
		}
	}
}

function updateX() {
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
	if (animationState == 1) { // if jump
		animator.SetInteger("state", animationState);
		animationState = 0;
	}
}