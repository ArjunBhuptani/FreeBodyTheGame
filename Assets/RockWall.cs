using UnityEngine;
using System.Collections;

public class RockWall : MonoBehaviour {

	private LevelManager levelManager;
	private Transform[] boulders;
	private Cart cart;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		boulders = this.GetComponentsInChildren<Transform>();
		cart = FindObjectOfType<Cart>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.name == "Cart"){
			Debug.Log("Cart hit wall");
			//some conditions about force of collision
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
		}
	}
}
