using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseHelper : MonoBehaviour {

    private Vector3 target;
    private int damage = 2;
    float speed = 100f;

	public void SetTarget (Vector3 t) {
        target.x = t.x;
        target.y = t.y;
	}
	
    public void increaseDamage(int increm)
    {
        damage += increm;
    }

    public int getDamage()
    {
        return damage;
    }

	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
            Destroy(gameObject);
	}

    private void OnCollisionEnter2D(Collision2D obj) {
        if (obj.gameObject.tag.Equals("Enemy")) {
            obj.gameObject.GetComponent<Health>().LoseHealth(damage);
        }
    }
}
