using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cart : MonoBehaviour {

	private float cartForce, distanceToWall;
	private float lastVelocity = 0;
	public float resistiveForce = 0;
	public float cartPlayerMass = 1;
	public bool wallBroken = false;

	private Objects cartObjects;
	private Character character;
	private bool isPowered, charInCart = false;
	private Text DistanceWall, ResistiveForce;
	private InputField ForwardForce;
	private GameObject RockWall;

	// Use this for initialization
	void Start () {
		cartObjects = this.GetComponent<Objects>();
		character = FindObjectOfType<Character>();
		DistanceWall = GameObject.Find("DistanceWall").GetComponent<Text>();
		ResistiveForce = GameObject.Find("ResistiveForce").GetComponent<Text>();
		ForwardForce = FindObjectOfType<InputField>();
		RockWall = GameObject.Find("Rock Wall");

		distanceToWall = Vector3.Distance(RockWall.transform.position, this.transform.position);

		DistanceWall.text = ("Distance to Wall: " + distanceToWall);
		ResistiveForce.text = ("Resistive Force: " + resistiveForce);
	}
	
	// Update is called once per frame
	void Update () {
		if(cartObjects.getPoweredBool()){
			isPowered = true;
		}

		if(isPowered && Vector3.Distance(character.transform.position, this.transform.position) <= 2){
			if(Input.GetKeyDown(KeyCode.E)){
				jumpInCart();
				charInCart = true;
			}
		}

		if(charInCart){
			calculateCartPosition();
		}

		if(wallBroken && Input.GetKeyDown(KeyCode.E)){
			resetCharacter();
		}
	}

	void jumpInCart(){
		character.GetComponent<Collider>().enabled = false;
		character.GetComponent<Rigidbody>().useGravity = false;
		character.GetComponent<Rigidbody>().isKinematic = true;
		character.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
		character.transform.parent = this.transform.parent;
	}

	void calculateCartPosition(){
		float.TryParse(ForwardForce.text, out cartForce);

		float finalForce = cartForce - resistiveForce;
		float acceleration = finalForce/cartPlayerMass;
		float currentVelocity = lastVelocity + acceleration * Time.deltaTime;

		//Debug.Log("CurrentVelocity: " + currentVelocity);
		float cartPosX = this.transform.position.x + currentVelocity*Time.deltaTime;
		lastVelocity = currentVelocity;

		this.transform.position = new Vector3(Mathf.Clamp(cartPosX,-3, 15), this.transform.position.y, this.transform.position.z);
		character.transform.position = this.transform.position;
	}

	void resetCharacter(){
		charInCart = false;
		character.GetComponent<Collider>().enabled = true;
		character.GetComponent<Rigidbody>().useGravity = true;
		character.GetComponent<Rigidbody>().isKinematic = false;
		character.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 2);
		character.transform.parent = null;
	}
}
