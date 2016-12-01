using UnityEngine;
using System.Collections;

public class Plank : MonoBehaviour {

	private Objects plankObjects;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		plankObjects = this.GetComponent<Objects>();
		rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (plankObjects.getLiftedBool()){
			//this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y - 1, this.transform.localPosition.z);
			//rb.constraints = RigidbodyConstraints.FreezePositionY;
		} else {
			//rb.constraints = RigidbodyConstraints.None;
		}
	}
}
