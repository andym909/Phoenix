using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAttack : MovingObject {

    GameObject player;
    private float speed = 2f;
    private float minDistance = 1f;
    private float range;
    private static int damage = 1;

	public float attackTimer = 2f;
	float timer = 0f;

	Animator anim;

	bool canSee;
	Vector3 ghostPlayer;

    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		anim = GetComponent<Animator>();
		ghostPlayer = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(canSee && player != null) {
			GetComponent<EnemyVision>().agro = true;
			ghostPlayer = player.transform.position;
			if(Vector2.Distance(transform.position, player.transform.position) <= minDistance && timer >= attackTimer) {
				anim.SetTrigger("attack");
				player.GetComponent<Health>().LoseHealth(damage);
				timer = 0f;
			}
			else {
				anim.SetInteger("movement", -1);
			}
		}
		if(player != null) {
			range = Vector2.Distance(transform.position, ghostPlayer);

			if(range > minDistance) {
				float xDist = ghostPlayer.x - transform.position.x;
				float yDist = ghostPlayer.y - transform.position.y;
				if(Mathf.Abs(xDist) > Mathf.Abs(yDist)) {
					if(xDist < 0)
						anim.SetInteger("movement", 3);
					else
						anim.SetInteger("movement", 1);
				}
				else {
					if(yDist < 0)
						anim.SetInteger("movement", 2);
					else
						anim.SetInteger("movement", 0);
				}
				transform.position = Vector2.MoveTowards(transform.position, ghostPlayer, speed * Time.deltaTime);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Wall") {
			speed = 1f;
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if(col.gameObject.tag == "Wall") {
			speed = 2f;
		}
	}

	public void SetCanSee(bool b) {
		canSee = b;
	}

	protected override void OnCantMove<T>(T component) {
		//Wall hitwall = component as Wall;
		//hitwall.Damagewall(wallDamage);
		//animator.SetTrigger("playerChop");
	}

    public void setDamage(int n) {
        damage = n;
    }
}
