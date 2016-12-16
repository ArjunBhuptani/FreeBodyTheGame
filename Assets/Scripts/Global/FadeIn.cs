using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {

	public float fadeInTime;

	[HideInInspector]
	public float winConditionTime = 0;

	private Image fadePanel;
	private Color currentColor = Color.black;
	private LevelManager levelManager;


	
	void Start () {
		fadePanel = GetComponent<Image>();
		levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.timeSinceLevelLoad < fadeInTime){
			Fade();
		} else{
			gameObject.SetActive (false);
		}
	}

	void Fade(){
		float alphaChange = Time.deltaTime/fadeInTime;
		currentColor.a -= alphaChange;
		fadePanel.color = currentColor;
	}
}
