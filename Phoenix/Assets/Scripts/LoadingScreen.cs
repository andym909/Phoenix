using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

	public Image load;
	GameObject canvas;

	public bool loading = true;
	float paddingTime = 1f;
	float paddingTimer = 0f;
	bool endingLoad = false;

	void Start() {
		canvas = GameObject.Find("Canvas");
		if(PlayerPrefs.GetInt("Start") == 1) {
			LoadScreen();
			PlayerPrefs.SetInt("Start", 0);
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
				print(paddingTimer);
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
