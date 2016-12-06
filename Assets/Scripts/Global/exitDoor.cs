using UnityEngine;
using System.Collections;

public class exitDoor : MonoBehaviour {

	private Character controller;
	private LevelManager levelManager;

	void Start(){
		controller = FindObjectOfType<Character>();
		levelManager = FindObjectOfType<LevelManager>();
	}

	void OnTriggerEnter(Collider col){
		if (col.name == controller.name){
			levelManager.setWinCondition();
			levelManager.LoadNextLevel();
		}
	}

}
