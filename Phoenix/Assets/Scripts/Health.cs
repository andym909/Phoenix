using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	int health;
	public bool canBeDamaged = true;

	public int startingHealth;

	void Awake () {
		SetHealth(startingHealth);
	}

	void Update () {
		if(!IsAlive()) {
			enabled = false;
		}
	}

	public void SetHealth(int start) {
		health = start;
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
	}

	public bool IsAlive() {
		return health > 0;
	}

	public bool CanBeDamaged() {
		return canBeDamaged;
	}

	public void SetCanBeDamaged(bool canBeDamaged) {
		this.canBeDamaged = canBeDamaged;
	}
}
