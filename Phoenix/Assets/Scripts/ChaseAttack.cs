using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 	A class that describes the behavior of the Enemy that chases the Player
 */

public class ChaseAttack : MovingObject {

    GameObject player;						// A reference to the Player object
    private float speed = 2f;				// The enemy's speed
    private float minDistance = 0.75f;		// The distance at which the enemy can attack the player
    private float range;					// A temp float used to determine how far the enemy is
											//		from the player at a given frame
    private static int damage = 1;			// How much damage the enemy type does

	public float attackTimer = 2f;			// A cooldown time for the enemy's attack
	float timer = 0f;						// The cooldown timer

	Animator anim;							// A reference to the Object's Animator component

	bool canSee;							// If the enemy can see the Player
	Vector3 ghostPlayer;					// The position the enemy travels to

    // Reference initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		anim = GetComponent<Animator>();
		ghostPlayer = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Update cooldown
		timer += Time.deltaTime;

		// Handles all movement, agro-ing, and animation of this enemy
		if(canSee && player != null) {
			GetComponent<EnemyVision>().agro = true;
			ghostPlayer = player.transform.position;
			if(Vector2.Distance(transform.position, player.transform.position) <= minDistance && timer >= attackTimer) {
				anim.SetTrigger("attack");
				anim.SetBool("movement", false);
				player.GetComponent<Health>().LoseHealth(damage);
				timer = 0f;
			}
		}
		if(player != null) {
			range = Vector2.Distance(transform.position, ghostPlayer);

			if(range > minDistance) {
				anim.SetBool("movement", true);
				float xDist = ghostPlayer.x - transform.position.x;
				float yDist = ghostPlayer.y - transform.position.y;
				if(Mathf.Abs(xDist) > Mathf.Abs(yDist)) {
					if(xDist < 0)
						anim.SetInteger("facing", 3);
					else
						anim.SetInteger("facing", 1);
				}
				else {
					if(yDist < 0)
						anim.SetInteger("facing", 2);
					else
						anim.SetInteger("facing", 0);
				}
				transform.position = Vector2.MoveTowards(transform.position, ghostPlayer, speed * Time.deltaTime);
			}
		}
	}

	// Slows enemy if in Wall
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Wall") {
			speed = 1f;
		}
	}

	// Speeds enemy when out of wall
	void OnCollisionExit2D(Collision2D col) {
		if(col.gameObject.tag == "Wall") {
			speed = 2f;
		}
	}

	// Changes canSee bool
	public void SetCanSee(bool b) {
		canSee = b;
	}

	// Tutorial remnant, removing this tends to break things so we don't
	protected override void OnCantMove<T>(T component) {
		//Wall hitwall = component as Wall;
		//hitwall.Damagewall(wallDamage);
		//animator.SetTrigger("playerChop");
	}

	// Set the amount of damage this enemy type can do
    public void setDamage(int n) {
        damage = n;
    }
}
