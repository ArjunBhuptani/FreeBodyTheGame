using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;
	
	private AudioSource audioSource;
	
	void Awake(){
		DontDestroyOnLoad (gameObject);
		Debug.Log ("Dont destroy on load:" + name);
	}
	
	void Start(){
		audioSource = GetComponent<AudioSource>();
		float volume = PlayerPrefsManager.GetMasterVolume();
		SetVolume (volume);
	}
		
	// Update is called once per frame
	void OnLevelWasLoaded(int level){
		AudioClip thisLevelMusic = levelMusicChangeArray[level];
		Debug.Log ("Playing clip: " +thisLevelMusic);
		
		if (thisLevelMusic){
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}
	
	public void SetVolume (float volume){
		audioSource.volume = volume;
	}
}
