using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 	Class to manage all objects with health (player and enemies)
 */ 

public class Health : MonoBehaviour {

	int health;							// The health count
	bool canBeDamaged = true;			// Bool that controls if object can be damaged
									
	int startingHealth;					// The starting health
	public GameObject feather;			// Reference to the feather for when an enemy dies

	SoundEffects se;					// Reference to the Sound Effects

	// Get the previous level's health
	void Awake () {
		SetHealth(PlayerPrefs.GetInt("Health"));
	}

	// Set SoundEffects referennce
	void Start() {
		se = Camera.main.GetComponent<SoundEffects>();
	}

	// Spawn a feather when an enemy dies
	void Update () {
		if(!IsAlive()) {
			if(this.gameObject.tag != "Player") {
				Instantiate(feather, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0), Quaternion.identity);
				Destroy(gameObject);
			}
			enabled = false;
		}
	}

	// Set health from another script
	public void SetHealth(int start) {
		health = start;
	}

	// Set starting health from another script
    public void setStartingHealth(int health) {
        this.startingHealth = health;
    }

	// Get object's health
	public int GetHealth() {
		return health;
	}

	// Increase object's health
	public void GainHealth(int delta) {
		health += delta;
	}

	// Reduce object's health, and play associated sound effects for player and enemy
	public void LoseHealth(int delta) {
		if(canBeDamaged)
			health -= delta;

		if(this.tag == "Player") {
			se.PlayPlayerHurt();
		}
		else if(this.tag == "Enemy") {
			se.PlayEnemyHurt();
		}
	}

	// Check if heath is below 0 and play death sounds if not
	public bool IsAlive() {
		bool alive = health > 0;

		if(!alive) {
			if(this.tag == "Player") {
				se.PlayPlayerDeath();
				se.PlayNecroDeath();
			}
			else if(this.tag == "Enemy") {
				se.PlayEnemyDeath();
			}
		}

		return alive;
	}

	// Return if the object can be damaged
	public bool CanBeDamaged() {
		return canBeDamaged;
	}

	// Set the canbedamaged bool
	public void SetCanBeDamaged(bool canBeDamaged) {
		this.canBeDamaged = canBeDamaged;
	}
}
