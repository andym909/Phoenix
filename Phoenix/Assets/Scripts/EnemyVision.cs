using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {

	GameObject player;
	Vector3 playerPos;

	ChaseAttack cAttack;
	ShootingAttack sAttack;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		playerPos = player.transform.position;

		cAttack = GetComponent<ChaseAttack>();
		sAttack = GetComponent<ShootingAttack>();
	}

	void Update() {
		if(player.transform.position != playerPos) {
			LookForPlayer();
		}
	}

	void LookForPlayer() {
		playerPos = player.transform.position;
		RaycastHit2D[] hits = new RaycastHit2D[1];
		GetComponent<Collider2D>().Raycast(new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y), hits);
		if(hits[0].transform != null && hits[0].transform.tag.Equals("Player")) {
			if(cAttack != null) {
				cAttack.SetCanSee(true);
			}
			if(sAttack != null) {
				sAttack.SetCanSee(true);
			}
		}
		else {
			if(cAttack != null) {
				cAttack.SetCanSee(false);
			}
			if(sAttack != null) {
				sAttack.SetCanSee(false);
			}
		}
	}
}
