#pragma strict

var speed = 0.1;
var animator : Animator;

function Start () {
	animator = GetComponent("Animator");
	
	// Workaround, the speed of the animation is randomly changed for 0.1 seconds so it seems like they start at different moments
	animator.speed = Random.Range(0,2000);
 	yield WaitForSeconds(0.1);	
    animator.speed = 1;
}

function Update () {
	updatePosition();
}

function updatePosition() {
	updateZ();
}

function updateZ() {
	GetComponent.<Rigidbody>().position.z += speed;
}