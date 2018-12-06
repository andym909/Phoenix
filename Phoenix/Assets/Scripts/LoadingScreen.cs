using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

	public Image load;
	GameObject canvas;
	SoundEffects se;

	public bool loading = true;
	float paddingTime = 1f;
	float paddingTimer = 0f;
	bool endingLoad = false;

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

	public void FinishLoad() {
		endingLoad = true;
	}

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
