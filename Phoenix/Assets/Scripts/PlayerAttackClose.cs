/*
 * This file controls the behavior of the player's melee attack
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackClose : MonoBehaviour {

    private float attDistance = 2.0f;
    public GameObject invisibleProjectile;

	SoundEffects se;
	MeleeEffects me;

	private float cooldown = 0.75f;
	float cooldownTimer;

	void Start() {
		se = Camera.main.GetComponent<SoundEffects>();
		me = GetComponentInChildren<MeleeEffects>();
		cooldownTimer = getCooldown();
	}

	void Update () {
		if(Camera.main.GetComponent<LoadingScreen>().GetLoading() == false && cooldownTimer >= getCooldown() && Input.GetButtonDown("Jump")) {
			// get the direction we're going
			// -1 idle, 0 up, 1 right, 2 down, 3 left
			int direction = this.GetComponent<Animator>().GetInteger("facing");
            // make a new projectile 
			GameObject tmp = (GameObject)Instantiate(invisibleProjectile, transform.position, Quaternion.identity);
			tmp.GetComponent<CloseHelper>().SetTarget(getTarget(direction));    // set the projectile's target
			GetComponent<Player>().ResetIdleTimer();  
			se.PlayPlayerMelee();
			me.MeleeAttackAnim();
			cooldownTimer = 0f;
		}
		else {
			cooldownTimer += Time.deltaTime;
		}
	}

    // determine where the target is depending on where the player is facing
    Vector3 getTarget(int dir) {
        Vector3 ret = this.transform.position;
        switch (dir) {
            case -1:
            case 2:
                ret.y -= getDistance();
                break;
            case 0:
                ret.y += getDistance();
                break;
            case 1:
                ret.x += getDistance();
                break;
            case 3:
                ret.x -= getDistance();
                break;
            default:
                print("Problem with the movement state machine");
                break;
        }
        return ret;
    }

    public float getDistance() {
        return attDistance;
    }

    public float getCooldown() {
        return cooldown;
    }
}
