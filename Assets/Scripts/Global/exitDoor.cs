using UnityEngine;
using System.Collections;

public class exitDoor : MonoBehaviour {

	private CharacterController controller;
	private LevelManager levelManager;

	void Start(){
		controller = FindObjectOfType<CharacterController>();
		levelManager = FindObjectOfType<LevelManager>();
	}

	void OnTriggerEnter(Collider col){
		if (col.name == controller.name){
			levelManager.LoadNextLevel();
		}
	}

}
