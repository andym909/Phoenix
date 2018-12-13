using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

	Health playerHealth;		// Reference to Player's Health instance
	Image[] fires;				// Array of the images that make up the Health HUD
	int curHealth;				// player's current health
	int maxHealth;				// player's maximum health
		
	// Need to be public to be set in-editor
	public Sprite fullFire;		// The full fire image
	public Sprite halfFire;		// the half a fire image
	public Sprite emptyFire;	// the outline of a fire image
	public Image fireHealth;	// An image prefab that has needed properties
	Canvas canvas;				// A reference to the UI canvas

	// curHealth setter and getter
	public void SetCurHealth(int h) {
		curHealth = h;
	}
	public int GetCurHealth() {
		return curHealth;
	}

	// maxHealth setter and getter
	public void SetMaxHealth(int h) {
		maxHealth = h;
	}
	public int GetMaxHealth() {
		return maxHealth;
	}

	// Set references and get health from previous scenes
	void Start () {
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		if(p != null) {
			playerHealth = p.GetComponent<Health>();
			maxHealth = PlayerPrefs.GetInt("MaxHealth");
			InitializeHealth();
		}
	}
		

	// Checks if health has been changed every frame
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
				maxHealth = PlayerPrefs.GetInt("MaxHealth");
				InitializeHealth();
			}
		}
	}

	// Update the visuals of health
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

	// Create the fire image array
	void InitializeHealth() {
		fires = new Image[maxHealth / 2];

		for(int i = 0; i < fires.Length; i++) {
			fires[i] = (Image)Instantiate(fireHealth, canvas.transform);
			fires[i].rectTransform.anchorMin = new Vector2(0f, 0f);
			fires[i].rectTransform.anchoredPosition = new Vector2(40f * (i+1), 40f);

			fires[i].enabled = false;
		}

		UpdateHealth();
	}

	// Increase max health
	public void IncreaseMax(int amount) {
		maxHealth += amount;
		fires = new Image[fires.Length + (amount / 2)];

		for(int i = 0; i < fires.Length; i++) {
			fires[i] = (Image)Instantiate(fireHealth, canvas.transform);
			fires[i].rectTransform.anchorMin = new Vector2(0f, 0f);
			fires[i].rectTransform.anchoredPosition = new Vector2(40f * (i+1), 40f);
		}

		PlayerPrefs.SetInt("MaxHealth", maxHealth);
		UpdateHealth();
	}
}
