using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public bool isLiftingSomething = false;

	private Objects[] roomObjects;
	private SpriteRenderer eKey;

	// Use this for initialization
	void Start () {
		eKey = GameObject.Find("EKey").GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		eKey.enabled = false;
		roomObjects = FindObjectsOfType<Objects>();
		foreach (Objects obj in roomObjects){
			checkPosition(obj);
		}
	}

	void checkPosition(Objects obj){
		if(obj != null){
			if(obj.isLiftable && Vector3.Distance(this.transform.position, obj.transform.position) <= obj.proximityRange){
				eKey.enabled = true;
			}

			if(obj.name == "Cart" && obj.getPoweredBool() && Vector3.Distance(this.transform.position, obj.transform.position) <= obj.proximityRange + 1){
				eKey.enabled = true;
			}
		}
	}
}
