using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

	public Image load;
	GameObject canvas;

	public bool loading = true;

	void Start() {
		canvas = GameObject.Find("Canvas");
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

	public void FinishLoad() {
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
	}

}
