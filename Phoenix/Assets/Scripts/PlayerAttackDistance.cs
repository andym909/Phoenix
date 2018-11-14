﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDistance : MonoBehaviour {

    private int damagePerAttack;
    private float attackDistance = 10f;

    public GameObject projectile;

	// Use this for initialization
	void Start () {
        damagePerAttack = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            // get the direction we're going
            // -1 idle, 0 up, 1 right, 2 down, 3 left
            int direction = this.GetComponent<Animator>().GetInteger("Movement");
            GameObject tmp = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
            tmp.GetComponent<Helper>().SetTarget(getTarget(direction));
        }
	}

    Vector3 getTarget(int dir) {
        Vector3 ret = this.transform.position;
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
}
