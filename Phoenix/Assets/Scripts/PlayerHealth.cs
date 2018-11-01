using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {

	public int startingHealth;

	void Start () {
		SetHealth(startingHealth);
	}

	void Update () {
		if(!IsAlive()) {
			Destroy(gameObject);
		}
	}
}
