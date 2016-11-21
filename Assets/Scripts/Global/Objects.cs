using UnityEngine;
using System.Collections;

public class Objects : MonoBehaviour {

	public bool isLiftable;
	public bool needsPower;
	public bool isMovable;
	public float proximityRange = 1f;

	private bool isLifted = false;
	private bool isPowered = false;
	private Character character;
	private Battery battery;
	private float originalHeight;

	// Use this for initialization
	void Start () {
		character = FindObjectOfType<Character>();

		// ***TO DO****need to find solution if there is more than one battery!
		battery = FindObjectOfType<Battery>();

		originalHeight = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		//lifting object conditions
		if(!isLifted){
			if (isLiftable && !character.isLiftingSomething){
				if (Input.GetKeyUp(KeyCode.E)){
					if (Vector3.Distance(character.transform.position, this.transform.position) <= proximityRange){
						liftObject();
					}
				}
			}//***TO DO*** EVERYTHING IS LIFTING WTF
		} else {
			if (isLiftable){
				if (Input.GetKeyUp(KeyCode.E)){
					dropObject();
				}
			}
		}

		//rotate when lifted
		if(isLifted){
			rotateObject();
		}

		if (!isPowered){
			if (needsPower){
				Objects batteryObjects = battery.GetComponent<Objects>();
				if (Vector3.Distance(battery.transform.position, this.transform.position) <= proximityRange){
					if (!batteryObjects.isLifted){
						Debug.Log(this.name);
						powerObject();
					}
				}
			}
		}
	
	}



	void liftObject(){
		//lift and attach to controller
		Collider col = GetComponent<Collider>();
		col.enabled = false;

		this.transform.position = new Vector3 (character.transform.position.x, character.transform.position.y + 1, character.transform.position.z);

		this.transform.parent = character.transform;

		isLifted = true;
		character.isLiftingSomething = true;
		Debug.Log("Lifting " + this.name);

		if(this.GetComponent<Rigidbody>() != null){
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.isKinematic = true;
			Debug.Log("disabling rb for " + this.name);
		}
	}

	void dropObject(){
		//drop and unattach from controller
		this.transform.SetParent(null);

		this.transform.position = new Vector3 (transform.position.x, originalHeight, transform.position.z);

		Collider col = GetComponent<Collider>();
		col.enabled = true;

		character.isLiftingSomething = false;
		isLifted = false;
		Debug.Log("Dropping " + this.name);

		if(this.GetComponent<Rigidbody>() != null){
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.isKinematic = false;
			Debug.Log("enabling rb for " +  this.name);
		}
	}

	void powerObject(){
		//destroy battery
		GameObject.DestroyImmediate(battery.gameObject);

		isPowered = true;
		Debug.Log("Powering" + this.name);
	}

	void rotateObject(){
		if (Input.GetKey(KeyCode.T)){
			this.transform.Rotate(Vector3.left*60*Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.Y)){
			this.transform.Rotate(Vector3.forward*60*Time.deltaTime);
		}
	}

	public bool getPoweredBool(){
		return isPowered;
	}

	public bool getLiftedBool(){
		return isLifted;
	}
}

