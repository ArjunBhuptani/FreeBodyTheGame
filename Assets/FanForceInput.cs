using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FanForceInput : MonoBehaviour {

	private Fan fan;
	private Text input;

	void Start(){
		fan = FindObjectOfType<Fan>();
		input = GameObject.Find("InputText").GetComponent<Text>();
	}

	public void fanForceInput(){
		float force;
		float.TryParse(input.text, out force);
		fan.forceValue = force;
	}
}
