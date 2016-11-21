using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBasket : MonoBehaviour {

	private float characterMass, basketMass, counterMass;
	private float currentVelocity = 0;
	private Rigidbody characterRB, basketRB;
	private Character character;
	private Text basketMassText, yourMassText;
	private InputField counterMassInput;
	private bool isClamped = true;

	// Use this for initialization
	void Start () {
		//find things
		character = FindObjectOfType<Character>();
		basketRB = this.GetComponentInChildren<Rigidbody>();
		characterRB = character.GetComponent<Rigidbody>();
		counterMassInput = FindObjectOfType<InputField>();

		//set basket mass text
		basketMass = basketRB.mass;
		basketMassText = GameObject.Find("BasketMass").GetComponent<Text>();
		basketMassText.text = ("Mass of Basket: " + basketMass);

		//randomize your mass and set text/rigidbody
		characterMass = Random.Range(20,70);
		characterRB.mass = characterMass;
		yourMassText = GameObject.Find("YourMass").GetComponent<Text>();
		yourMassText.text = ("Your Mass: " + characterMass);
	}

	// Update is called once per frame
	void Update () {
		//set counter mass
		float.TryParse(counterMassInput.text, out counterMass);
		if(this.transform.position.y >= 10.4){
			isClamped = true;
		}

		if(!isClamped){
			counterMassInput.enabled = false;
			calculatePosition();
		}
	}

	public void clampButton(){
		isClamped = !isClamped;
		Debug.Log("Clamped: " + isClamped);
	}

	void calculatePosition(){
		float massDifference = counterMass - (characterMass + basketMass);
		float tensionForce = 9.81f*massDifference;
		float acceleration = tensionForce/(characterMass+basketMass);

		currentVelocity = currentVelocity + (acceleration*Time.deltaTime);

		transform.Translate(0, currentVelocity ,0);
	}
}
