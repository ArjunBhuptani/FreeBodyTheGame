using UnityEngine;
using System.Collections;

public class BoulderTrolley : MonoBehaviour {

	private Rigidbody rb;
	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		rb = GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezePositionZ;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.x <= -2 || this.transform.position.x >= 2){
			levelManager.setWinCondition();
		}
	}
}
