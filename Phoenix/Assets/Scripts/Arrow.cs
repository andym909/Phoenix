using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	Vector3 target;
	float speed = 5f;
    private int damage = 1;

	public void SetTarget(Vector3 t, int damage) {
		target = t;
        this.damage = damage;
	}

	void Update() {
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
		if(transform.position == target) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag.Equals("Player")) {
			other.gameObject.GetComponent<Health>().LoseHealth(damage);
			Destroy(gameObject);
		}
	}
}
