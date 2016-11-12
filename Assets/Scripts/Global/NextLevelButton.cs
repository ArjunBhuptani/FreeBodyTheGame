using UnityEngine;
using System.Collections;

public class NextLevelButton : MonoBehaviour {

	public void nextLevel(){
		LevelManager levelManager = FindObjectOfType<LevelManager>();
		levelManager.setWinCondition();
		levelManager.LoadNextLevel();
	}

}
