using UnityEngine;
using System.Collections;

public class CustomCharacterController : MonoBehaviour {

	[System.Serializable]
	public class MoveSetting{
		public float forwardVel = 12;
		public float rotateVel = 100;
		public float jumpVel = 10;
		public float distToGrounded = 1f;
		public LayerMask ground;
	}

	[System.Serializable]
	public class PhysSetting{
		public float downAccel = 0.75f;
	}

	[System.Serializable]
	public class InputSetting{
		public float inputDelay = 0.1f;
		public string FORWARD_AXIS = "Vertical";
		public string TURN_AXIS = "Horizontal";
		public string JUMP_AXIS= "Jump";
	}

	public MoveSetting moveSetting = new MoveSetting();
	public PhysSetting physSetting = new PhysSetting();
	public InputSetting inputSetting = new InputSetting();

	Vector3 velocity = Vector3.zero;
	Quaternion targetRotation;
	Rigidbody rBody;
	CustomCameraControl cameraControl;
	float forwardInput, turnInput, jumpInput;

	public Quaternion TargetRotation
	{
		get{return targetRotation;}
	}

	bool Grounded(){
		return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
	}

	void Start () {
		targetRotation = transform.rotation;
		if(GetComponent<Rigidbody>()){
			rBody = GetComponent<Rigidbody>();
		} else {
			Debug.LogError("The character needs a rigidbody.");
		}

		if(FindObjectOfType<CustomCameraControl>()){
			cameraControl = FindObjectOfType<CustomCameraControl>();
		} else {
			Debug.LogError("No camera found");
		}

		forwardInput = turnInput = jumpInput = 0;
	}
	
	void GetInput(){
		forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS); //interpolated
		turnInput = Input.GetAxis(inputSetting.TURN_AXIS); //interpolated
		jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS); //non-interpolated
	}

	void Update () {
		GetInput();
		Turn();
	}

	void FixedUpdate(){
		Run();
		Jump();

		rBody.velocity = transform.TransformDirection(velocity);
	}

	void Run(){
		if (Mathf.Abs(forwardInput) > inputSetting.inputDelay){
			//move
			cameraControl.orbit.yRotation = Mathf.Lerp(cameraControl.transform.eulerAngles.y, -180, cameraControl.position.lookSmooth*Time.deltaTime);
			velocity.z = moveSetting.forwardVel*forwardInput;
		} else {
			velocity.z = 0;
		}
	}

	void Turn(){
		if (Input.GetKey(KeyCode.Mouse0)){
			if (Mathf.Abs(forwardInput) > inputSetting.inputDelay){
				float horizontal = Input.GetAxis(cameraControl.input.ORBIT_HORIZONTAL) * cameraControl.orbit.hOrbitSmooth * Time.deltaTime;
				transform.Rotate(new Vector3(0, horizontal, 0));
				targetRotation = transform.rotation;
			}
		} else {
			if (Mathf.Abs(turnInput) > inputSetting.inputDelay){
				targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel*turnInput*Time.deltaTime, Vector3.up);
			}
			transform.rotation = targetRotation;
		}
	}

	void Jump(){
		if(jumpInput> 0 && Grounded()){
			//jump
			velocity.y = moveSetting.jumpVel;
		} else if (jumpInput == 0 && Grounded()){
			//zero out our velocity.y
			velocity.y = 0;
		} else {
			//(falling) decrease velocity.y
			velocity.y -= physSetting.downAccel;
		}
	}
}
