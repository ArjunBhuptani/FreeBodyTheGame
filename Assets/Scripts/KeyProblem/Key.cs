using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	private KeyLauncher keyLauncher;

	// Use this for initialization
	void Start () {
		keyLauncher = FindObjectOfType<KeyLauncher>();
		this.transform.parent = keyLauncher.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
