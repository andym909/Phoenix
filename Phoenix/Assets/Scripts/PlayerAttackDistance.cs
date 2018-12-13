/*
 * This file controls the behavior of the player's ranged attack
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDistance : MonoBehaviour {

    private float attackDistance = 10f;

    public GameObject projectile;

	private float cooldown = 0.75f;
	float cooldownTimer;

	SoundEffects se;

	// Use this for initialization
	void Start () {
		cooldownTimer = getCooldown();
		se = Camera.main.GetComponent<SoundEffects>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Camera.main.GetComponent<LoadingScreen>().GetLoading() == false && cooldownTimer >= getCooldown() && Input.GetButtonDown("Fire1")) {
			// get the direction we're going
			// -1 idle, 0 up, 1 right, 2 down, 3 left
			int direction = this.GetComponent<Animator>().GetInteger("facing");
            // make a projectile object and set the target
			GameObject tmp = (GameObject)Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 226f - direction * 90f)));
			tmp.GetComponent<Helper>().SetTarget(getTarget(direction));
			GetComponent<Player>().ResetIdleTimer();
			cooldownTimer = 0f;
			se.PlayFireballShoot();
		}
		else {
			cooldownTimer += Time.deltaTime;
		}
	}

    // get the target based on the direction the player's facing
    Vector3 getTarget(int dir) {
        Vector3 ret = this.transform.position;
        // -1 idle, 0 up, 1 right, 2 down, 3 left
        switch (dir) {
            case -1:
            case 2:
                ret.y -= attackDistance;
                break;
            case 0:
                ret.y += attackDistance;
                break;
            case 1:
                ret.x += attackDistance;
                break;
            case 3:
                ret.x -= attackDistance;
                break;
            default:
                print("Problem with the movement state machine");
                break;
        }
        return ret;
    }

    public float getCooldown() {
        return cooldown;
    }
}
