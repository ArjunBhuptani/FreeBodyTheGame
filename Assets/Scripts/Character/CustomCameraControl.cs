﻿using UnityEngine;
using System.Collections;

public class CustomCameraControl : MonoBehaviour {

	public Transform target;

	[System.Serializable]
	public class PositionSettings{
		public Vector3 targetPosOffset = new Vector3(0,0,0);
		public float lookSmooth = 100f;
		public float distanceFromTarget = -8;
		public float zoomSmooth = 100;
		public float maxZoom = -2;
		public float minZoom = -15;
		public bool smoothFollow = true;
		public float smooth = 0.05f;

		[HideInInspector]
		public float newDistance = -8;
		[HideInInspector]
		public float adjustmentDistance = -8;
	}

	[System.Serializable]
	public class OrbitSettings{
		public float xRotation = -20;
		public float yRotation = -180;
		public float maxXRotation = 25;
		public float minXRotation = -85;
		public float vOrbitSmooth = 0.04f;
		public float hOrbitSmooth = 0.04f;
	}

	[System.Serializable]
	public class InputSettings{
		public string ORBIT_HORIZONTAL_SNAP = "OrbitHorizontalSnap";
		public string ORBIT_HORIZONTAL = "OrbitHorizontal";
		public string ORBIT_VERTICAL = "OrbitVertical";
		public string ZOOM = "Mouse ScrollWheel";
	}

	[System.Serializable]
	public class DebugSettings{
		public bool drawDesiredCollisionLines = true;
		public bool drawAdjustedCollisionLines = true;
	}

	public PositionSettings position = new PositionSettings();
	public OrbitSettings orbit = new OrbitSettings();
	public InputSettings input = new InputSettings();
	public DebugSettings debug = new DebugSettings();
	public CollisionHandler collision = new CollisionHandler();

	Vector3 targetPos = Vector3.zero;
	Vector3 destination = Vector3.zero;
	Vector3 adjustedDestination = Vector3.zero;
	Vector3 camVel = Vector3.zero;
	CustomCharacterController charController;
	float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;


	void Start () {
		SetCameraTarget(target);

		vOrbitInput = hOrbitInput = zoomInput = hOrbitSnapInput = 0;

		MoveToTarget();

		collision.Initialize(Camera.main);
		collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
		collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);
	}

	public void SetCameraTarget(Transform t){
		target = t;

		if (target != null){
			if(target.GetComponent<CustomCharacterController>()){
				charController = target.GetComponent<CustomCharacterController>();
			} else {
				Debug.LogError("The camera's target needs a character controller.");
			}
		} else {
			Debug.LogError("Camera needs a target.");
		}
	}

	void GetInput(){
		vOrbitInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
		hOrbitInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL);
		hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL_SNAP);
		zoomInput = Input.GetAxisRaw(input.ZOOM);

	}

	void Update(){
		GetInput();
		ZoomInOnTarget();
	}

	void FixedUpdate () {
		//moving
		MoveToTarget();
		//rotating
		LookAtTarget();
		//orbiting
		OrbitTarget();

		collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
		collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);

		//draw debug lines
		for(int i = 0; i < 5; i++){
			if (debug.drawDesiredCollisionLines){
				Debug.DrawLine(targetPos, collision.desiredCameraClipPoints[i], Color.white);
			}
			if (debug.drawAdjustedCollisionLines){
				Debug.DrawLine(targetPos, collision.adjustedCameraClipPoints[i], Color.green);
			}
		}

		collision.CheckColliding(targetPos); //using raycasts here
		position.adjustmentDistance = collision.GetAdjustedDistanceWithRayFrom(targetPos);
	}

	void MoveToTarget(){
		targetPos = target.position + position.targetPosOffset;
		destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
		destination += targetPos;

		if (collision.colliding){
			adjustedDestination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * Vector3.forward * position.adjustmentDistance;
			adjustedDestination += targetPos;

			if(position.smoothFollow){
				//use smooth damp function
				transform.position = Vector3.SmoothDamp(transform.position, adjustedDestination, ref camVel, position.smooth);
			} else { 
				transform.position = adjustedDestination;
			}
		} else {
			if(position.smoothFollow){
				//use smooth damp function
				transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, position.smooth);
			} else { 
				transform.position = destination;
			}
		}
	}

	void LookAtTarget(){
		Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmooth*Time.deltaTime);
	}

	void OrbitTarget(){
		if (hOrbitSnapInput > 0){
			orbit.yRotation = -180;
		}

		if (Input.GetKey(KeyCode.Mouse0)){
			orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
			orbit.yRotation += -hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

			orbit.xRotation = Mathf.Clamp(orbit.xRotation, orbit.minXRotation, orbit.maxXRotation);
		}
	}

	void ZoomInOnTarget(){
		position.distanceFromTarget += zoomInput * position.zoomSmooth * Time.deltaTime;

		position.distanceFromTarget = Mathf.Clamp(position.distanceFromTarget, position.minZoom, position.maxZoom);
	}

	[System.Serializable]
	public class CollisionHandler{

		public LayerMask collisionLayer;

		[HideInInspector]
		public bool colliding = false;
		[HideInInspector]
		public Vector3[] adjustedCameraClipPoints;
		[HideInInspector]
		public Vector3[] desiredCameraClipPoints;

		Camera camera;

		public void Initialize(Camera cam){
			camera = cam;
			adjustedCameraClipPoints = new Vector3[5];
			desiredCameraClipPoints = new Vector3[5];
		}

		public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray){
			if (!camera)
				return;

			//clear the contents of intoArray
			intoArray = new Vector3[5];

			float z = camera.nearClipPlane;
			float x = Mathf.Tan(camera.fieldOfView/3.41f) * z;
			float y = x / camera.aspect;

			//top left
			intoArray[0] = (atRotation * new Vector3(-x,y,z)) + cameraPosition; //added and rotated the point relative to camera
			//top right
			intoArray[1] = (atRotation * new Vector3(x,y,z)) + cameraPosition;
			//bottom left
			intoArray[2] = (atRotation * new Vector3(-x,-y,z)) + cameraPosition;
			//bottom right
			intoArray[3] = (atRotation * new Vector3(x,-y,z)) + cameraPosition;
			//camera's position
			intoArray[4] = cameraPosition - camera.transform.forward;
		}

		bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition){
			for (int i = 0; i < clipPoints.Length; i++){
				Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
				float distance = Vector3.Distance(clipPoints[i], fromPosition);
				if (Physics.Raycast(ray, distance, collisionLayer)){
					return true;
				}
			}

			return false;
		}


		public float GetAdjustedDistanceWithRayFrom(Vector3 from){
			float distance = -1;

			for (int i = 0; i < desiredCameraClipPoints.Length; i++){
				Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)){
					if (distance == -1){
						distance = hit.distance;
					} else {
						if (hit.distance < distance){
							distance = hit.distance;
						}
					}
				}
			}

			if (distance == -1) {
				return 0;
			} else {
				return distance;
			}
		}

		public void CheckColliding(Vector3 targetPosition){
			if (CollisionDetectedAtClipPoints(desiredCameraClipPoints, targetPosition)){
				colliding = true;
			} else {
				colliding = false;
			}
		}
	}

}
