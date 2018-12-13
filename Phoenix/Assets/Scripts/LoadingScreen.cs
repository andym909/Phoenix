using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 	This scripts handles the logic behind our loading screen, and starts the loading process
 * 
 */ 

public class LoadingScreen : MonoBehaviour {

	public Image load;	// Reference to the load screen picture
	GameObject canvas;	// Reference to the UI Canvas
	SoundEffects se;	// Reference to the SoundEffects script

	bool loading = true;		// Boolean describing if loading is occurring
	float paddingTime = 1f;		// Padding time to let the game completely load before the player can move
	float paddingTimer = 0f;	// Timer for the above
	bool endingLoad = false;	// Boolean to start that timer

	// loading setter and getter
	public void SetLoading(bool b) {
		loading = b;
	} 
	public bool GetLoading() {
		return loading;
	}

	// Set SoundEffects reference and play starting sounds
	void Start() {
		se = Camera.main.GetComponent<SoundEffects>();
		canvas = GameObject.Find("Canvas");
		if(PlayerPrefs.GetInt("Start") == 1) {
			LoadScreen();
			PlayerPrefs.SetInt("Start", 0);
			se.PlayNecroStart();
		}
		else if(PlayerPrefs.GetInt("Start") == 2) {
			LoadScreen();
			se.PlayNecroStartAgain();
			PlayerPrefs.SetInt("Start", 0);
		}
		else {
			se.PlayNecroLoad();
		}
	}

	// Remove HUD elements and pull up the loading screen
	public void LoadScreen() {
		Image[] images = canvas.GetComponentsInChildren<Image>();
		for(int i = 0; i < images.Length; i++) {
			if(!images[i].Equals(load))
				images[i].enabled = false;
		}
		Text[] texts = canvas.GetComponentsInChildren<Text>();
		for(int i = 0; i < texts.Length; i++) {
			texts[i].enabled = false;
		}

		load.enabled = true;
		loading = true;
	}

	// Move through the padding timer
	void Update() {
		if(endingLoad) {
			if(paddingTimer < paddingTime) {
				paddingTimer += Time.deltaTime;
			}
			else {
				paddingTimer = 0f;
				EndLoad();
			}
		}
	}

	// Start the padding timer
	public void FinishLoad() {
		endingLoad = true;
	}

	// Pull the HUD back up and remove the loading screen
	void EndLoad() {
		if(canvas == null) {
			canvas = GameObject.Find("Canvas");
		}
		Image[] images = canvas.GetComponentsInChildren<Image>();
		for(int i = 0; i < images.Length; i++) {
			if(!images[i].Equals(load))
				images[i].enabled = true;
		}
		Text[] texts = canvas.GetComponentsInChildren<Text>();
		for(int i = 0; i < texts.Length; i++) {
			texts[i].enabled = true;
		}
		load.enabled = false;
		loading = false;
		endingLoad = false;
	}

}
