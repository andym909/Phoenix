using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {

	bool tutorial = false;
	Image tutorialImage;

	void Start() {
		PlayerPrefs.SetInt("MaxHealth", 10);
		PlayerPrefs.SetInt("Health", 10);
		PlayerPrefs.SetInt("Feathers", 0);
		PlayerPrefs.SetInt("Start", PlayerPrefs.GetInt("Start") == -1 ? 2 : 1);
		PlayerPrefs.SetInt("Speed", 4);
		PlayerPrefs.SetInt("MeleeDmg", 4);
		PlayerPrefs.SetInt("RangeDmg", 2);
		PlayerPrefs.SetInt("Level", 1);

		tutorialImage = GameObject.Find("TutorialImage").GetComponent<Image>();
	}

	void Update() {
		if(Input.anyKeyDown) {
			if(tutorial) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
			else {
				tutorialImage.enabled = true;
				tutorial = true;
			}
		}
	}

}
