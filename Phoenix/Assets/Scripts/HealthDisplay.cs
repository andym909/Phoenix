using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

	Health playerHealth;
	Image[] fires;
	int curHealth;
	int maxHealth;

	public Sprite fullFire;
	public Sprite halfFire;
	public Sprite emptyFire;

	public Image fireHealth;
	Canvas canvas;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		if(p != null) {
			playerHealth = p.GetComponent<Health>();
			maxHealth = playerHealth.GetHealth();
			UpdateMaxHealth();
		}
	}

	void Update () {
		if(playerHealth != null) {
			int newHealth = playerHealth.GetHealth();
			if(newHealth != curHealth) {
				curHealth = newHealth;
				UpdateHealth();
			}
		}
		else {
			GameObject p = GameObject.FindGameObjectWithTag("Player");
			if(p != null) {
				playerHealth = p.GetComponent<Health>();
				maxHealth = playerHealth.GetHealth();
				UpdateMaxHealth();
			}
		}
	}

	void UpdateHealth() {
		int healthCounter = 0;
		int difference;
		foreach(Image fire in fires) {
			difference = curHealth - healthCounter;
			if(difference > 1) {
				fire.sprite = fullFire;
				healthCounter += 2;
			}
			else if(difference > 0) {
				fire.sprite = halfFire;
				healthCounter += 1;
			}
			else {
				fire.sprite = emptyFire;

			}
		}
	}

	void InitializeHealth() {
		curHealth = playerHealth.GetHealth();
		fires = new Image[curHealth / 2];

		for(int i = 0; i < fires.Length; i++) {
			fires[i] = (Image)Instantiate(fireHealth, canvas.transform);
			fires[i].rectTransform.anchorMin = new Vector2(0f, 0f);
			fires[i].rectTransform.anchoredPosition = new Vector2(40f * (i+1), 40f);
		}
	}

	public void IncreaseMax(int amount) {
		fires = new Image[fires.Length + (amount / 2)];

		for(int i = 0; i < fires.Length; i++) {
			fires[i] = (Image)Instantiate(fireHealth, canvas.transform);
			fires[i].rectTransform.anchorMin = new Vector2(0f, 0f);
			fires[i].rectTransform.anchoredPosition = new Vector2(40f * (i+1), 40f);
		}

		UpdateHealth();
	}
}
