using UnityEngine;
using System.Collections;

public class BoulderTrolley : MonoBehaviour {

	private Rigidbody rb;
	private LevelManager levelManager;
	private Fan fan;
	private Vector3 startPostion;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		fan = FindObjectOfType<Fan>();
		rb = GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezePositionZ;
		startPostion = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position != startPostion){
			fan.transform.parent = this.transform;
		}

		if (this.transform.position.x <= -2 || this.transform.position.x >= 2){
			levelManager.setWinCondition();
		}
	}
}
