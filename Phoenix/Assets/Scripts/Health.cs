using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	int health;

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
		health -= delta;
	}

	public bool IsAlive() {
		return health > 0;
	}
}
