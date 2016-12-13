using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyLauncher : MonoBehaviour {

	public Key keyPrefab;

	private Key key;
	private float angle, velocity, startAngle;
	private Button button;
	private Text velocityText, angleText;
	private bool rotating = false;

	// Use this for initialization
	void Start () {
		GameObject buttonObject = GameObject.Find("SpawnKey");
		GameObject vTextObject = GameObject.Find("Velocity");
		GameObject aTextObject = GameObject.Find("Angle");
		button = buttonObject.GetComponent<Button>();
		velocityText = vTextObject.GetComponent<Text>();
		angleText = aTextObject.GetComponent<Text>();

		button.interactable = true;
		LoadNextKey();
	}
	
	// Update is called once per frame
	void Update () {
		if(rotating){
			float finalAngle = 90f - angle;
			float angleDifference = finalAngle - startAngle;
			float speed = angleDifference/1.5f;
			transform.Rotate(Vector3.forward*speed*Time.deltaTime);
			float currentAngle = 90-transform.eulerAngles.z;
			if(transform.eulerAngles.z <= finalAngle + 0.5 && transform.eulerAngles.z >= finalAngle - 0.5){
				rotating = false;
				activateButton();
			}
		}
	}

	void SpawnKey(){
		Destroy(GameObject.Find("Key(Clone)"));
		Instantiate(keyPrefab);
	}

	public void LaunchKey(){
		//launch the key
		SpawnKey();
		key = FindObjectOfType<Key>();
		Rigidbody rb = key.GetComponent<Rigidbody>();
		rb.isKinematic = false;
		Debug.Log("Launched with angle: " + angle + " and velocity: " + velocity); 	
		rb.velocity = new Vector3 (velocity*Mathf.Sin(Mathf.Deg2Rad*(90f-angle)), velocity*Mathf.Cos(Mathf.Deg2Rad*angle), 0);

		//set up for next key launch
		LoadNextKey();
	}

	void LoadNextKey(){
		//**disable button
		activateButton();

		//**set new angle and velocity
		angle = Random.Range(20,70);
		velocity = 10;

		//**push angle and velocity to UI
		velocityText.text = ("Velocity: " + velocity);
		angleText.text = ("Angle: " + angle);

		//**move launcher to angle slowly
		startAngle = this.transform.rotation.eulerAngles.z;
		rotating = true;

	}

	void activateButton(){
		button.interactable = !button.interactable;
	}
}
