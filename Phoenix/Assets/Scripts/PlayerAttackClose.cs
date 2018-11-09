using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackClose : MonoBehaviour {

    public int damagePerAttack = 2;
    private GameObject[] enemies;
    public float attDistance = 1.5f;
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
            // get the direction we're going
            // -1 idle, 0 up, 1 right, 2 down, 3 left
            int direction = this.GetComponent<Animator>().GetInteger("facing");
            enemies = GameObject.FindGameObjectsWithTag("Enemy");   // array of enemies
            foreach (GameObject enemy in enemies) {
				if(isInDirection(enemy, direction)) {
					print("Hitting Enemy!");
					enemy.GetComponent<Health>().LoseHealth(damagePerAttack);
				}
            }
        }
	}

	private bool isInDirection(GameObject enemy, int dir) {
		switch(dir) {
			case -1:
			case 2:
				return (enemy.transform.position.y < this.transform.position.y &&
					enemy.transform.position.y > (this.transform.position.y - attDistance));
			case 3:
				return (enemy.transform.position.x < this.transform.position.x &&
					enemy.transform.position.x > (this.transform.position.x - attDistance));
			case 0:
				return (enemy.transform.position.y > this.transform.position.y &&
					enemy.transform.position.y < (this.transform.position.y + attDistance));
			case 1:
				return (enemy.transform.position.x > this.transform.position.x &&
					enemy.transform.position.x < (this.transform.position.x + attDistance));
			default:
				return false;
		}
	}
}
