using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour {

	private bool isPowered = false;
	private bool isLifted = false;

	private Objects fanObjects;
	private Objects[] roomObjects;
	private Transform fanBlades;

	// Use this for initialization
	void Start () {
		fanObjects = GetComponent<Objects>();
		roomObjects = FindObjectsOfType<Objects>();

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
			if(obj.isMovable && Vector3.Distance(this.transform.position, obj.transform.position) <= obj.proximityRange){
				Debug.Log("obj name" + obj.name);
				this.transform.parent = obj.transform;
				Debug.Log("adding force");
				//add force in direction of forward fan
				addForce(obj);
			}
		}
	}

	void addForce(Objects obj){
		Collider col = this.GetComponent<Collider>();
		col.enabled = false;

		Rigidbody rb = this.GetComponentInParent<Rigidbody>();
		rb.isKinematic = false;
		rb.AddForce(transform.up*80);

	}
}
