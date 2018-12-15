/*
 * This file controls the projectile fired by the player's distance attack
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour {

    private Vector3 target;
    static float speed;
    private static int damage;

    // get the damage value from playerprefs
	void Start() {
		IncreaseSpeed(PlayerPrefs.GetInt("Speed"));
		damage = PlayerPrefs.GetInt("RangeDmg");
	}

    // set the target for the projectile
    public void SetTarget(Vector3 t) {
        target.x = t.x;
        target.y = t.y;
        // z is always 0;
        target.z = t.z;
    }
	
	void Update () {
        // move toward the target with each frame
        // if it gets to the target, destroy it
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
            Destroy(gameObject);
	}

    // increase the damage done by the projectile
    public void increaseDamage(int increm)
    {
        damage += increm;
		PlayerPrefs.SetInt("RangeDmg", damage);
    }

    public int getDamage()
    {
        return damage;
    }

	public static void IncreaseSpeed(int playerSpeed) {
		speed = 8 + (playerSpeed - 4);
	}

    private void OnCollisionEnter2D(Collision2D obj) {
        // if it hits an enemy, damage the enemy
		if(obj.gameObject.tag.Equals("Enemy")) {
			obj.gameObject.GetComponent<Health>().LoseHealth(damage);
		}
        // if it hits anything besides the player, destroy it
		if(!obj.gameObject.tag.Equals("Player")) {
			Destroy(gameObject);
		}
	}
}
