using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public float autoLoadNextLevelAfter;

	private bool winCondition = false;
	
	void Start(){
		if (autoLoadNextLevelAfter == 0){
			Debug.Log ("Level auto load disabled");
		}else {
		Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}

	public void setWinCondition(){
		winCondition = true;
	}

	public bool getWinCondition(){
		return winCondition;
	}

	public void LoadLevel(string name){
		if(winCondition){
			Debug.Log ("New Level load: " + name);
			SceneManager.LoadScene(name);
		}
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		//Application.Quit ();
	}
	
	public void LoadNextLevel(){
		if(winCondition){
			Debug.Log("Loading next level");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	public void ResetLevel(){
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.buildIndex);
	}

}
