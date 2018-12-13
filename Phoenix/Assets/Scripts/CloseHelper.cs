/*
 * This file controls the projectile fired by the player's melee attack
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseHelper : MonoBehaviour {

    private Vector3 target;
    private static int damage;  
    float speed = 20f;     // speed is high so it happens fast 

    // get the current damage value from playerprefs
	void Start() {
		damage = PlayerPrefs.GetInt("MeleeDmg");
	}

    // set the location of where the projectile will go
	public void SetTarget (Vector3 t) {
        target.x = t.x;
        target.y = t.y;
	}
	
    // increase the damage done by the attack
    public void increaseDamage(int increm)
    {
        damage += increm;
		PlayerPrefs.SetInt("MeleeDmg", damage);
    }

    public int getDamage()
    {
        return damage;
    }

	void Update () {
        // move closer to the target with each frame
        // if it makes it to the target, destroy it
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
            Destroy(gameObject);
	}

    private void OnCollisionStay2D(Collision2D obj) {
        // if it hits an enemy, damage the enemy
        if (obj.gameObject.tag.Equals("Enemy")) {
            obj.gameObject.GetComponent<Health>().LoseHealth(damage);
        }
    }
}
