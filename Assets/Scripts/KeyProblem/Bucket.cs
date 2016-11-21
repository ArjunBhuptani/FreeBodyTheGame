using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bucket : MonoBehaviour {

	private LevelManager levelManager;
	private Text text;

	void Start(){
		levelManager = FindObjectOfType<LevelManager>();
		GameObject distanceText = GameObject.Find("Distance");
		text = distanceText.GetComponent<Text>();
	}

	void Update(){
		float distance = this.transform.position.x - (-5f);
		text.text = ("Distance: " + Mathf.Round(distance));
	}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.name == ("Key(Clone)")){
			//play key in bucket animation
			Debug.Log("Key in bucket");
			levelManager.setWinCondition();
		}
	}

}
