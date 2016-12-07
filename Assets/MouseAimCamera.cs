using UnityEngine;
using System.Collections;

public class MouseAimCamera : MonoBehaviour {
    public GameObject target;
    public float rotateSpeed = 5;
    Vector3 offset;
     
    void Start() {
        offset = new Vector3 (0f, -3f, 5.5f);
    }
     
    void LateUpdate() {
    	if(Input.GetKey(KeyCode.Mouse0)){
        	float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
       		target.transform.Rotate(0, horizontal, 0);
       	}
 
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);
         
        transform.LookAt(target.transform);
    }
}