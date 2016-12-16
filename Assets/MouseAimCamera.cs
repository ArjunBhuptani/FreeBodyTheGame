using UnityEngine;
using System.Collections;

public class MouseAimCamera : MonoBehaviour {

    public GameObject target;
    public float rotateSpeed = 5;
    Vector3 offset;
    bool movingCamera = false;
    bool thirdPerson = false;
    float t = 0;
     
    void Start() {
        offset = new Vector3 (0f, -3f, 5.5f);
        Invoke("SwitchCamera", 2);

    }

    void SwitchCamera(){
    	movingCamera = !movingCamera;
    }
     
    void LateUpdate() {
    	if(thirdPerson){
    		if(Input.GetKey(KeyCode.Mouse0)){
        		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
       			target.transform.Rotate(0, horizontal, 0);
      	 	}
 
        	float desiredAngle = target.transform.eulerAngles.y;
       		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        	transform.position = target.transform.position - (rotation * offset);
         
        	transform.LookAt(target.transform);
        }

        if(movingCamera){
			if (t<=4){
				t+=Time.deltaTime;
				float desiredAngle = target.transform.eulerAngles.y;
       			Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        		transform.position = Vector3.Slerp(transform.position, target.transform.position - (rotation*offset), t);
				transform.LookAt(target.transform);
			} else {
				thirdPerson = true;
			}
        }
    }
}