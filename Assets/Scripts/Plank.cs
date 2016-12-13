using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Plank : MonoBehaviour {

	private Objects plankObjects;
	private Rigidbody rb;
	private Text distanceWall;

	// Use this for initialization
	void Start () {
		plankObjects = this.GetComponent<Objects>();
		rb = this.GetComponent<Rigidbody>();
		distanceWall = GameObject.Find("DistanceWall").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (plankObjects.getLiftedBool()){
			//this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y - 1, this.transform.localPosition.z);
			//rb.constraints = RigidbodyConstraints.FreezePositionY;
		} else {
			//rb.constraints = RigidbodyConstraints.None;
		}

		float distance = Mathf.Round((5f - this.transform.position.x)*10f)/10f;
		distanceWall.text = ("Distance to Wall: " + distance);
	}
}
