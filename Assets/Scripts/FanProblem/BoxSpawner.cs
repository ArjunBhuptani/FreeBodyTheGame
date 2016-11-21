using UnityEngine;
using System.Collections;

public class BoxSpawner : MonoBehaviour {

	public MirrorBox mirrorBox;

	public void SpawnBox(){
		if(!GameObject.FindObjectOfType<MirrorBox>())
			Instantiate(mirrorBox);
		else {
			Destroy(GameObject.Find("MirrorBox(Clone)"));
			Instantiate(mirrorBox);
		}
	}
}
