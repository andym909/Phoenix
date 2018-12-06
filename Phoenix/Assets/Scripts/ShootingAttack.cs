﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAttack : MonoBehaviour {

	GameObject player;
	public float timer = 1f;
	float timeElapsed = 0f;
    private static int damage = 1;
	public float range = 15f;

	public GameObject projectile;
	Animator anim;

	bool canSee = false;

	SoundEffects se;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		anim = GetComponent<Animator>();
		se = Camera.main.GetComponent<SoundEffects>();
	}

	void Update () {
		if(Vector3.Distance(player.transform.position, transform.position) < range) {	
			anim.SetBool("isAttacking", true);
			if(canSee && timeElapsed >= timer && player != null) {
				float atan = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
				Vector3 angle = new Vector3(0f, 0f, atan * (180f / Mathf.PI) + 140f); //WHO KNOWS WHY ITS + 140, IT JUST IS
				GameObject tmp = (GameObject)Instantiate(projectile, transform.position, Quaternion.Euler(angle));
				tmp.GetComponent<Arrow>().SetTarget(player.transform.position, damage);
				timeElapsed = 0f;
				se.PlayFireballShoot();
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

	public void SetCanSee(bool b) {
		canSee = b;
	}

    public void setDamage(int n) {
        damage = n;
    }
}
