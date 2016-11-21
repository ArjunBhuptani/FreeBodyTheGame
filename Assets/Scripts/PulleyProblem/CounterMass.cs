using UnityEngine;
using System.Collections;

public class CounterMass : MonoBehaviour {

	private PlayerBasket playerBasket;
	// Use this for initialization
	void Start () {
		playerBasket = FindObjectOfType<PlayerBasket>();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(0, -playerBasket.transform.position.y, 0);
	}
}
