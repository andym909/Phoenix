using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {

	GameObject player;			// Reference to Player object
	Vector3 playerPos;			// To store player position each frame

	ChaseAttack cAttack;		// Reference to the ChaseAttack script
	ShootingAttack sAttack;		// Reference to the ShootingAttack script

	bool agro = false;			// Boolean if the enemy has been agro'd

	// Initialize References
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		playerPos = player.transform.position;

		cAttack = GetComponent<ChaseAttack>();
		sAttack = GetComponent<ShootingAttack>();
	}

	// Search for the Player if previously could not see
	void Update() {
		if(player.transform.position != playerPos) {
			LookForPlayer();
		}
	}

	// bool agro setter
	public void changeAgro(bool b) {
		agro = b;
	}

	// Method to search for player using raycasts
	void LookForPlayer() {
		playerPos = player.transform.position;
		RaycastHit2D[] hits = new RaycastHit2D[10];
		GetComponent<Collider2D>().Raycast(new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y), hits);
		//Can see through walls if agro'd
		int index = agro ? 10 : 1;

		bool seen = false;
		// Update attack scripts
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
