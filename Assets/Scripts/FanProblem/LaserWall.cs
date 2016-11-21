using UnityEngine;
using System.Collections;

public class LaserWall : MonoBehaviour {

	private MirrorBox mirrorBox;
	private GameObject laser2;
	private ParticleSystem ps;
	private Vector3 originalPosition;

	// Use this for initialization
	void Start () {
		laser2 = GameObject.Find("Laser2");
		ps = laser2.GetComponent<ParticleSystem>();
		originalPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindObjectOfType<MirrorBox>() != null){
			mirrorBox = GameObject.FindObjectOfType<MirrorBox>();
			if (mirrorBox.transform.position.y >= 1.5 && mirrorBox.transform.position.y <= 2.5){
				mirrorBox.GetComponent<Collider>().enabled = false;
				this.transform.position = new Vector3(0f, 0.5f, 5.5f);
				var em = ps.emission;
				em.enabled = true;
			} else{
				this.transform.position = originalPosition;
				mirrorBox.GetComponent<Collider>().enabled = true;
				var em = ps.emission;
				em.enabled = false;
			}
		}
	}
}
