using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour {

	public float forceValue = 50;

	private bool isPowered = false;
	private bool isLifted = false;

	private Objects fanObjects;
	private Objects[] roomObjects;
	private Transform fanBlades;
	private SpriteRenderer bolt;

	// Use this for initialization
	void Start () {
		bolt = this.GetComponentInChildren<SpriteRenderer>();
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
		bolt.transform.Rotate(Vector3.up*90*Time.deltaTime);

		isPowered = fanObjects.getPoweredBool();
		isLifted = fanObjects.getLiftedBool();

		if(isPowered){
			rotateBlades();
			bolt.enabled = false;

			if(!isLifted){
				roomObjects = FindObjectsOfType<Objects>();
				foreach(Objects obj in roomObjects){
					checkPosition(obj);
				}
			}
		}

	}

	void rotateBlades(){
		fanBlades.Rotate(Vector3.up*180*Time.deltaTime);
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
		Collider col = this.GetComponent<Collider>();
		col.enabled = false;

		Rigidbody rb = obj.GetComponent<Rigidbody>();
		rb.isKinematic = false;
		rb.AddForce(transform.up*forceValue);

	}
}
