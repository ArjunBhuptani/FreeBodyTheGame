using UnityEngine;
using System.Collections;

public class LaserReceiver : MonoBehaviour {

	private int chargingCounter = 0;
	private Renderer ren;
	private LevelManager levelManager;

	void Start(){
		ren = this.GetComponent<Renderer>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		ren.material.color = new Color(0,0,0,0);
	}

	void Update(){
		if (chargingCounter >= 50){
			levelManager.setWinCondition();
		}
	}

	void OnParticleCollision(GameObject other){
		chargingCounter++;
		if(chargingCounter <= 50){
			//***FIX Color ISSUE***
			ren.material.color = new Color(256/50*chargingCounter,0,0,256/50*chargingCounter);
		}
	}
}
