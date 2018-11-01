using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackClose : MonoBehaviour {

    private int damagePerAttack;
    private GameObject[] enemies;


	void Start () {
        damagePerAttack = 1;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
            // get the direction we're going
            // -1 idle, 0 up, 1 right, 2 down, 3 left
            int direction = this.GetComponent<Animator>().GetInteger("Movement");
            enemies = GameObject.FindGameObjectsWithTag("Enemy");   // array of enemies
            foreach (GameObject enemy in enemies) {
                switch (direction) {
                    case -1:
                    case 2:
                        if (isDown(enemy))
                            print("hit enemy down");
                        break;
                    case 0:
                        if (isUp(enemy))
                            print("hit enemy up");
                        break;
                    case 1:
                        if (isRight(enemy))
                            print("hit enemy right");
                        break;
                    case 3:
                        if (isLeft(enemy))
                            print("hit enemy left");
                        break;
                    default:
                        print("Whoa buddy, you broke the movement state machine");
                        break;
                }
            }
        }
	}

    // check if enemy is above you
    private bool isUp(GameObject enemy) {
        return (enemy.transform.position.y > this.transform.position.y &&
                enemy.transform.position.y < (this.transform.position.y + 1.0));
    }

    // check if enemy is right of you 
    private bool isRight(GameObject enemy) {
        return (enemy.transform.position.x > this.transform.position.x &&
                enemy.transform.position.x < (this.transform.position.x + 1.0));
    }

    // check if enemy is left of you
    private bool isLeft(GameObject enemy) {
        return (enemy.transform.position.x < this.transform.position.x &&
                enemy.transform.position.x > (this.transform.position.x - 1.0));
    }

    // check if enemy is below you
    private bool isDown(GameObject enemy) {
        return (enemy.transform.position.y < this.transform.position.y &&
                enemy.transform.position.y > (this.transform.position.y - 1.0));
    }
}
