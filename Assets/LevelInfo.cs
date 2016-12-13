using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour {

	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		Scene scene = SceneManager.GetActiveScene();
		text.text = ("Level name: " + scene.name);
	}

}
