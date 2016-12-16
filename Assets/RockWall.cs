using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RockWall : MonoBehaviour {

	public float minForce = 14;
	public float maxForce = 18;

	private LevelManager levelManager;
	private Transform[] boulders;
	private Cart cart;

	[HideInInspector]
	public float finalForce;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		boulders = this.GetComponentsInChildren<Transform>();
		cart = FindObjectOfType<Cart>();

		GameObject.Find("MinForce").GetComponent<Text>().text = ("Minimum Force to Break Wall: " + minForce);
		GameObject.Find("MaxForce").GetComponent<Text>().text = ("Max Force Seatbelts Can Withstand: " + maxForce);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col){
		if (col.collider.name == "Cart"){
			Debug.Log("Cart hit wall with force: " + finalForce);
			//some conditions about force of collision
			if( finalForce >= minForce && finalForce <= maxForce){
				levelManager.setWinCondition();
				foreach(Transform boulder in boulders){
					boulder.parent = null;
					boulder.gameObject.AddComponent<SphereCollider>();
					boulder.gameObject.AddComponent<Rigidbody>();
					boulder.GetComponent<Rigidbody>().mass = 100;
					boulder.GetComponent<Rigidbody>().drag = 1;
				}
				cart.wallBroken = true;
				Destroy(this.GetComponent<Collider>());
				Destroy(this.GetComponent<Rigidbody>());
				Destroy(this);
			} else {
				this.GetComponent<Rigidbody>().isKinematic = true;
			}
		}
	}
}
