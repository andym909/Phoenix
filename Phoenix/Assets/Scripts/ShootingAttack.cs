using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAttack : MonoBehaviour {

	GameObject player;
	public float timer = 1f;
	float timeElapsed = 0f;

	public float range = 15f;

	public GameObject projectile;
	Animator anim;

	void Start() {
		player = GameObject.Find("Player");
		anim = GetComponent<Animator>();
	}

	void Update () {
		if(Vector3.Distance(player.transform.position, transform.position) < range) {	
			anim.SetBool("isAttacking", true);
			if(timeElapsed >= timer && player != null) {
				GameObject tmp = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
				tmp.GetComponent<Arrow>().SetTarget(player.transform.position);
				timeElapsed = 0f;
			}
			else {
				timeElapsed += Time.deltaTime;
			}
		}
		else {
			anim.SetBool("isAttacking", false);
			timeElapsed = 0f;
		}
	}
}
