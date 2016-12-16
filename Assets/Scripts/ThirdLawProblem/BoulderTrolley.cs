using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoulderTrolley : MonoBehaviour {

	private Rigidbody rb;
	private LevelManager levelManager;
	private Fan fan;
	private Vector3 startPostion;
	private GameObject fanSocket;
	private Text powerInstructions, movableInstructions;
	private FanPositionMarker fanMarker;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		fan = FindObjectOfType<Fan>();
		fanSocket = GameObject.Find("FanSocket");
		rb = GetComponent<Rigidbody>();
		powerInstructions = GameObject.Find("PowerInstructions").GetComponent<Text>();
		movableInstructions = GameObject.Find("MovableInstructions").GetComponent<Text>();
		fanMarker = FindObjectOfType<FanPositionMarker>();

		rb.constraints = RigidbodyConstraints.FreezePositionZ;
		startPostion = this.transform.position;
		Invoke("ActivatePowerInstructions", 4);
	}
	
	// Update is called once per frame
	void Update () {
		if (!fan.GetComponent<Objects>().getLiftedBool() && Vector3.Distance(startPostion, fan.transform.position) <= 2){
			Debug.Log("what");
			fan.transform.position = fanSocket.transform.position;
			fan.transform.parent = fanSocket.transform;
		}

		if (this.transform.position.x <= -2 || this.transform.position.x >= 2){
			movableInstructions.enabled = false;
			levelManager.setWinCondition();
		} else {
			if (fan.GetComponent<Objects>().getPoweredBool()){
				powerInstructions.enabled = false;
				Invoke("ActivateMovableInstructions", 4);
			}
		}
	}

	void ActivatePowerInstructions(){
		powerInstructions.enabled = true;
	}

	void ActivateMovableInstructions(){
		fanMarker.GetComponent<MeshRenderer>().enabled = true;
		movableInstructions.enabled = true;
	}
}
