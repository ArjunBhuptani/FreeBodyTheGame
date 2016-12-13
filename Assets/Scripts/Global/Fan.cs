using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour {

	public float forceValue;

	private bool isPowered = false;
	private bool isLifted = false;

	private Objects fanObjects;
	private Objects[] roomObjects;
	private Transform fanBlades;


	// Use this for initialization
	void Start () {
		fanObjects = GetComponent<Objects>();

		foreach(Transform child in transform){
			if (child.name == "fan1"){
				Transform fan1 = child;
				foreach(Transform grandchild in fan1.transform){
					fanBlades = grandchild;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		isPowered = fanObjects.getPoweredBool();
		isLifted = fanObjects.getLiftedBool();

		if(isPowered){
			rotateBlades();

			if(!isLifted){
				roomObjects = FindObjectsOfType<Objects>();
				foreach(Objects obj in roomObjects){
					checkPosition(obj);
				}
			}
		}

	}

	void rotateBlades(){
		fanBlades.Rotate(Vector3.up*320*Time.deltaTime);
	}

	void checkPosition(Objects obj){
		if(obj != null){
			if(obj.isMovable && Vector3.Distance(this.transform.position, obj.transform.position) <= obj.proximityRange && !obj.getLiftedBool()){
				Debug.Log("obj name" + obj.name);
				//this.transform.parent = obj.transform;
				Debug.Log("adding force");
				//add force in direction of forward fan
				addForce(obj);
			}
		}
	}

	void addForce(Objects obj){

		Rigidbody objRB = obj.GetComponent<Rigidbody>();
		objRB.isKinematic = false;
		objRB.AddForce(transform.up*forceValue);

	}
}
