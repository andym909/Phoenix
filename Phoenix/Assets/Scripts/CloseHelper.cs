using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseHelper : MonoBehaviour {

    private Vector3 target;
    private int damage = 2;
    float speed = 20f;

	public void SetTarget (Vector3 t) {
        target.x = t.x;
        target.y = t.y;
	}
	
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
            Destroy(gameObject);
	}

    private void OnCollisionStay2D(Collision2D obj) {
		print(obj.gameObject.tag);
        if (obj.gameObject.tag.Equals("Enemy")) {
            obj.gameObject.GetComponent<Health>().LoseHealth(damage);
        }
    }
}
