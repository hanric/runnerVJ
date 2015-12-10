using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trees : MonoBehaviour {

	public GameObject tree;
	List<GameObject> trees;

	float leftXTree = 8.0f;
	float rightXTree = 14.0f;

	public GameObject plane;
	List<GameObject> planes;

	// Use this for initialization
	void Start () {
		trees = new List<GameObject> ();
		planes = new List<GameObject> ();
		InvokeRepeating ("createTreeAtRandomTime", 0.0f, 1.0f);
		createFirstPlanes ();
		createPlane ();
		InvokeRepeating ("createPlane", 0.0f, 5.25f);
	}
	
	// Update is called once per frame
	void Update () {
		updatePositions ();
		deleteOldTrees ();
		deleteOldPlanes ();
	}

	void createTreeAtRandomTime() {
		Invoke ("createTree", Random.Range (1.2f, 2.5f));
	}

	void createTree() {
		int side = Random.Range (1, 3);
		float treeX;
		if (side == 1)
			treeX = leftXTree;
		else
			treeX = rightXTree;

		GameObject treeClone = (GameObject) Instantiate (tree, new Vector3 (treeX, 0, 25), new Quaternion(0,0,0,0));
		trees.Add (treeClone);
	}

	void createFirstPlanes() {
		GameObject planeClone = (GameObject) Instantiate (plane, new Vector3 (7.5f, 0.0f, 5.0f), new Quaternion (0, 0, 0, 0));
		GameObject planeClone2 = (GameObject) Instantiate (plane, new Vector3 (7.5f, 0.0f, 35.0f), new Quaternion (0, 0, 0, 0));
		planes.Add (planeClone);
		planes.Add (planeClone2);
	}

	void createPlane() {
		GameObject planeClone = (GameObject) Instantiate (plane, new Vector3 (7.5f, 0.0f, 65.0f), new Quaternion (0, 0, 0, 0));
		planes.Add (planeClone);
	}

	void updatePositions() {
		foreach (GameObject aTree in trees) {
			aTree.transform.position -= new Vector3(0.0f, 0.0f, 0.1f);
		}
		foreach (GameObject aPlane in planes) {
			aPlane.transform.position -= new Vector3(0.0f, 0.0f, 0.1f);
		}
	}

	void deleteOldTrees() {
		List<GameObject> treesToDelete = new List<GameObject> ();
		foreach (GameObject aTree in trees) {
			if (aTree.transform.position.z < 0) {
				treesToDelete.Add(aTree);
			}
		} 
		foreach (GameObject treeToDelete in treesToDelete) {
			Destroy(treeToDelete);
			trees.Remove(treeToDelete);
		}
	}

	void deleteOldPlanes() {
		List<GameObject> planesToDelete = new List<GameObject> ();
		foreach (GameObject aPlane in planes) {
			if (aPlane.transform.position.z < -15) {
				planesToDelete.Add(aPlane);
			}
		} 
		foreach (GameObject planeToDelete in planesToDelete) {
			Destroy(planeToDelete);
			planes.Remove(planeToDelete);
		}
	}
}
