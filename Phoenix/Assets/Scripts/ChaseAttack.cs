﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAttack : MonoBehaviour {

    public GameObject player;
    public float speed = 2f;
    private float minDistance = 1f;
    private float range;

	public float attackTimer = 2f;
	float timer = 0f;

	Animator anim;

    // Use this for initialization
    void Awake () {
        player = GameObject.Find("Player");
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(player != null) {
			range = Vector2.Distance(transform.position, player.transform.position);	
			if(range > minDistance) {
				float xDist = player.transform.position.x - transform.position.x;
				float yDist = player.transform.position.y - transform.position.y;
				if(Mathf.Abs(xDist) > Mathf.Abs(yDist)) {
					if(xDist < 0)
						anim.SetInteger("movement", 3);
					else
						anim.SetInteger("movement", 1);
				}
				else {
					if(yDist < 0)
						anim.SetInteger("movement", 2);
					else
						anim.SetInteger("movement", 0);
				}
				transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
			}
			else if(timer >= attackTimer) {
				anim.SetTrigger("attack");
				player.GetComponent<Health>().LoseHealth(1);
				timer = 0f;
			}
			else {
				anim.SetInteger("movement", -1);
			}
		}
	}
}
