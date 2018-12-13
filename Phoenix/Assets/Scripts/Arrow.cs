using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A Class that describes the behavior of the enemy projectile
 */

public class Arrow : MonoBehaviour {

	Vector3 target;				// The destination the Arrow is headed to
	float speed = 5f;			// Arrow's speed
    private int damage = 1;		// How much damage the arrow does on contact

	// When Arrow is created, the creator also calls SetTarget to imitate a Constructor
	//		Note: An actual constructor cannot be used due to Unity's Object hierarchy
	public void SetTarget(Vector3 t, int damage) {
		target = t;
        this.damage = damage;
	}

	//Called every frame, updates position towards the target
	void Update() {
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
		if(transform.position == target) {
			Destroy(gameObject);
		}
	}

	//Called when collides with another collider, if its the player, player hit hit logic occurs
	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag.Equals("Player")) {
			other.gameObject.GetComponent<Health>().LoseHealth(damage);
			Destroy(gameObject);
		}
	}
}
