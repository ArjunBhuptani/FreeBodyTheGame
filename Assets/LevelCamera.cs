using UnityEngine;
using System.Collections;

public class LevelCamera : MonoBehaviour {

	private MouseAimCamera Camera;

	// Use this for initialization
	void Start () {
		Camera = FindObjectOfType<MouseAimCamera>();
		Invoke("ThirdPersonCamera", 4);
	}

	void ThirdPersonCamera(){
		Camera.enabled = true;
	}

}
