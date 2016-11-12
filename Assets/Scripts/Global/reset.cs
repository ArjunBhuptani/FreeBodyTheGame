using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour {

	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
	}
	
	public void resetLevel(){
		levelManager.ResetLevel();
	}
}
