using UnityEngine;
using System.Collections;

public class LevelCamera : MonoBehaviour {

	private MouseAimCamera Camera;
	private GameObject target;

	// Use this for initialization
	void Start () {
		Camera = FindObjectOfType<MouseAimCamera>();
		target = GameObject.Find("ThirdPersonController");
		Vector3 offset = new Vector3 (0f, -3f, 5.5f);
	}

	void FixedUpdate(){
		
	}

	void ThirdPersonCamera(){
		Vector3 offset = new Vector3 (0f, -3f, 5.5f);
		float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        float t = 0f;

        while (t<=4){
        	Debug.Log("Lerping");
        	t+=Time.deltaTime;
        	transform.position = Vector3.Lerp(transform.position, target.transform.position - (rotation*offset), t);
			transform.LookAt(target.transform);
        }
         
		Camera.enabled = true;
	}

}
