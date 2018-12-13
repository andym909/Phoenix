using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 	A class that describes the behavior of the Enemy that shoots at the Player
 * 
 */ 

public class ShootingAttack : MonoBehaviour {

	GameObject player;				// Reference to the player object
	public float timer = 1f;		// Cooldown time (public so it can be changed in-editor)
	float timeElapsed = 0f;			// Cooldown timer
	private static int damage = 1;	// Damage done by the projectiles
	public float range = 15f;		// Range of the projectiles (public so it can be changed in-editor)

	public GameObject projectile;	// Reference to the projectile prefab
	Animator anim;					// Reference to the animator

	bool canSee = false;			// If the enemy can see the player

	SoundEffects se;				// Reference to the sound effects script


	// Set references
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		anim = GetComponent<Animator>();
		se = Camera.main.GetComponent<SoundEffects>();
	}

	void Update () {
		// Attacks if in visible, in range, and the cooldown has elapsed
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

	// Setter for canSee
	public void SetCanSee(bool b) {
		canSee = b;
	}

	// Setter for damage
    public void setDamage(int n) {
        damage = n;
    }
}
