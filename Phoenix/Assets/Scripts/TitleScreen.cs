using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	void Start() {
		PlayerPrefs.SetInt("MaxHealth", 10);
		PlayerPrefs.SetInt("Health", 10);
		PlayerPrefs.SetInt("Feathers", 0);
		PlayerPrefs.SetInt("Start", 1);
	}

	void Update() {
		if(Input.anyKey) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

}
