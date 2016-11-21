using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MirrorBox : MonoBehaviour {


	private float width, height;
	private int mass;
	private Rigidbody rb;
	private BoxSpawner boxSpawner;
	private Objects objects;
	private Text text;

	void Start(){
		width = Random.Range(0.75f,1.25f);
		height = Random.Range(0.75f,1.25f);
		mass = Mathf.RoundToInt(Random.Range(5,15));

		this.transform.localScale = new Vector3 (width, width, height);	
		rb = GetComponent<Rigidbody>();
		rb.mass = mass;

		text = FindObjectOfType<Text>();
		text.text = "Mass of Mirror Box: " + mass;	

		boxSpawner = GameObject.FindObjectOfType<BoxSpawner>();
		this.transform.SetParent(boxSpawner.transform);

		this.transform.position = new Vector3 (transform.position.x, height/2f + 0.1f, transform.position.z);

		objects = this.GetComponent<Objects>();

	}

	void Update(){
		if(!objects.getLiftedBool()){
			this.transform.SetParent(boxSpawner.transform);
		}
	}
}
