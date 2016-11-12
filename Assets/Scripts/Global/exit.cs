using UnityEngine;
using System.Collections;

public class exit : MonoBehaviour {

	private LevelManager levelManager;
	private Component[] colliders;
	private Component[] renderers;


	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();

	}
	
	// Update is called once per frame
	void Update () {
		if(levelManager.getWinCondition()){
			showExit();
	}
}

	void showExit(){
		colliders = this.GetComponentsInChildren<Collider>();
		renderers = this.GetComponentsInChildren<Renderer>();

		foreach (Collider col in colliders)
			col.enabled = true;

		foreach (Renderer ren in renderers)
			ren.enabled = true;
	}

}
