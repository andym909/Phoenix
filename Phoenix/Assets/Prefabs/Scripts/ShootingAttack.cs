using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAttack : MonoBehaviour {

	GameObject player;
	public float timer = 1f;
	float timeElapsed = 0f;

	public GameObject projectile;

	void Start() {
		player = GameObject.Find("Player");
	}

	void Update () {
		if(timeElapsed >= timer) {
			GameObject tmp = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
			tmp.GetComponent<Arrow>().SetTarget(player.transform.position);
			timeElapsed = 0f;
		}
		else {
			timeElapsed += Time.deltaTime;
		}
	}
}
