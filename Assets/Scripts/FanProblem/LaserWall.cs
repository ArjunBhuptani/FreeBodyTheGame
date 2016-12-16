using UnityEngine;
using System.Collections;

public class LaserWall : MonoBehaviour {

	private MirrorBox mirrorBox;
	private GameObject laser2;
	private ParticleSystem ps;
	private Vector3 originalPosition;
	private Fan fan;
	private FanPositionMarker fanMarker;

	// Use this for initialization
	void Start () {
		laser2 = GameObject.Find("Laser2");
		ps = laser2.GetComponent<ParticleSystem>();
		originalPosition = this.transform.position;
		fan = FindObjectOfType<Fan>();
		fanMarker = FindObjectOfType<FanPositionMarker>();
	}
	
	// Update is called once per frame
	void Update () {
		if(fan.GetComponent<Objects>().getPoweredBool()){
			if (Vector3.Distance(fan.transform.position, fanMarker.transform.position) >= 2 || fan.GetComponent<Objects>().getLiftedBool()){
				fanMarker.GetComponent<MeshRenderer>().enabled = true;
			} else {
				fanMarker.GetComponent<MeshRenderer>().enabled = false;
			}
		}

		if(GameObject.FindObjectOfType<MirrorBox>() != null){
			mirrorBox = GameObject.FindObjectOfType<MirrorBox>();
			Debug.Log("MirrorBox position: "+ mirrorBox.transform.position);
			if (mirrorBox.transform.position.y >= 1.5 && mirrorBox.transform.position.y <= 2.5 && mirrorBox.transform.position.x <= 1.5 && mirrorBox.transform.position.x >= -1.5 && mirrorBox.transform.position.z <= 1.5 && mirrorBox.transform.position.z >= -1.5){
				Debug.Log("Deflecting");
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
			if (mirrorBox.transform.position.y > 3){
				GameObject.Destroy(mirrorBox.gameObject);
				//play shatter sound and animation
			}
		}
	}
}
