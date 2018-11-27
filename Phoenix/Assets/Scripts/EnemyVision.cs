using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {

	GameObject player;
	Vector3 playerPos;

	ChaseAttack cAttack;
	ShootingAttack sAttack;
	[System.NonSerialized]
	public bool agro = false;

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
		RaycastHit2D[] hits = new RaycastHit2D[10];
		GetComponent<Collider2D>().Raycast(new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y), hits);
		int index = agro ? 10 : 1;
		bool seen = false;
		for(int i = 0; i < index; i++) {
			if(hits[i].transform != null && hits[i].transform.tag.Equals("Player")) {
				seen = true;
				if(cAttack != null) {
					cAttack.SetCanSee(true);
				}
				if(sAttack != null) {
					sAttack.SetCanSee(true);
				}
			}
		}
		if(!seen) {
			if(cAttack != null) {
				cAttack.SetCanSee(false);
			}
			if(sAttack != null) {
				sAttack.SetCanSee(false);
			}
		}
	}
}
