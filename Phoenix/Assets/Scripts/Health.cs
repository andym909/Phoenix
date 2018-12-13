using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

	int health;
	public bool canBeDamaged = true;

	public int startingHealth;
	public GameObject feather;

	SoundEffects se;

	void Awake () {
		SetHealth(PlayerPrefs.GetInt("Health"));
	}

	void Start() {
		se = Camera.main.GetComponent<SoundEffects>();
	}

	void Update () {
		if(!IsAlive()) {
			if(this.gameObject.tag != "Player") {
				Instantiate(feather, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0), Quaternion.identity);
				Destroy(gameObject);
			}
			enabled = false;
		}
	}

	public void SetHealth(int start) {
		health = start;
	}

    public void setStartingHealth(int health) {
        this.startingHealth = health;
    }

	public int GetHealth() {
		return health;
	}

	public void GainHealth(int delta) {
		health += delta;
	}

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

	public bool CanBeDamaged() {
		return canBeDamaged;
	}

	public void SetCanBeDamaged(bool canBeDamaged) {
		this.canBeDamaged = canBeDamaged;
	}
}
