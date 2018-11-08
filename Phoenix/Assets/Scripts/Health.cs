using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	int health;
	public bool canBeDamaged = true;

	public int startingHealth;

	void Start () {
		SetHealth(startingHealth);
	}

	void Update () {
		if(!IsAlive()) {
			Destroy(gameObject);
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
		print("Attempting Losing Health");
		if(canBeDamaged)
			print("Loss of health");
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
