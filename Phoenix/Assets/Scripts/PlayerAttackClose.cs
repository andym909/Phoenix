using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackClose : MonoBehaviour {

    public float attDistance = 2.0f;
    public GameObject invisibleProjectile;
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
            // get the direction we're going
            // -1 idle, 0 up, 1 right, 2 down, 3 left
            int direction = this.GetComponent<Animator>().GetInteger("facing");
            GameObject tmp = (GameObject)Instantiate(invisibleProjectile, transform.position, Quaternion.identity);
            tmp.GetComponent<CloseHelper>().SetTarget(getTarget(direction));
        }
	}

    Vector3 getTarget(int dir) {
        Vector3 ret = this.transform.position;
        switch (dir) {
            case -1:
            case 2:
                ret.y -= attDistance;
                break;
            case 0:
                ret.y += attDistance;
                break;
            case 1:
                ret.x += attDistance;
                break;
            case 3:
                ret.x -= attDistance;
                break;
            default:
                print("Problem with the movement state machine");
                break;
        }
        return ret;
    }
}
