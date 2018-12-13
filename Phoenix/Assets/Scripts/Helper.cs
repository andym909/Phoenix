using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour {

    private Vector3 target;
    private static int damage = 1;
    static float speed = 8f;

	void Start() {
		damage = PlayerPrefs.GetInt("RangeDmg");
	}

    public void SetTarget(Vector3 t) {
        // make sure x direction is within the board
        if (t.x < 0)
            target.x = 0;
        else if (t.x > GameManager.instance.boardScript.columns)
            target.x = GameManager.instance.boardScript.columns;
        else
            target.x = t.x;

        // make sure y direction is within the board
        if (t.y < 0)
            target.y = 0;
        else if (t.y > GameManager.instance.boardScript.rows)
            target.y = GameManager.instance.boardScript.rows;
        else
            target.y = t.y;

        // z is always 0;
        target.z = t.z;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
            Destroy(gameObject);
	}

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
		if(obj.gameObject.tag.Equals("Enemy")) {
			obj.gameObject.GetComponent<Health>().LoseHealth(damage);
		}
		if(!obj.gameObject.tag.Equals("Player")) {
			Destroy(gameObject);
		}
	}
}
