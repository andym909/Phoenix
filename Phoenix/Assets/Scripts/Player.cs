using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;
    bool hasKey = false;

    private Animator animator;
    private int food;

	float idleTime = 2f;
	float idleTimer = 0f;

	// Use this for initialization
	protected override void Start () {
		GetComponent<Health>().SetCanBeDamaged(true);

        animator = GetComponent<Animator>();

        food = GameManager.instance.playerFoodPoints;

        base.Start();
	}

    private void OnDisable() {
        GameManager.instance.playerFoodPoints = food;
    }

    // Update is called once per frame
    void Update () {
        if (!GameManager.instance.playersTurn)
            return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

		if(horizontal != 0) {
			animator.SetBool("movement", true);
			vertical = 0;

			if(horizontal > 0) {
				animator.SetInteger("facing", 1);
			}
			else {
				animator.SetInteger("facing", 3);
			}
		}
		else if(vertical > 0) {
			animator.SetBool("movement", true);
			animator.SetInteger("facing", 0);
		}
		else if(vertical < 0) {
			animator.SetBool("movement", true);
			animator.SetInteger("facing", 2);
		}
		else {
			animator.SetBool("movement", false);
		}

		if(horizontal != 0 || vertical != 0) {
			idleTimer = 0f;
			AttemptMove<Wall>(horizontal, vertical);
		}
		else {
			idleTimer += Time.deltaTime;
			if(idleTimer >= idleTime) {
				animator.SetInteger("facing", 2);
			}
			StopMotion();
		}
	}

    protected override void AttemptMove<T>(int xDir, int yDir) {
        food--;
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;

        checkIfGameOver();

        GameManager.instance.playersTurn = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Exit" && hasKey == true) {
			GetComponent<Health>().SetCanBeDamaged(false);
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food") {
            food += pointsPerFood;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Soda") {
            food += pointsPerSoda;
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "Key")
        {
            hasKey = true;
            other.gameObject.SetActive(false);
			GameObject.FindGameObjectWithTag("Exit").GetComponent<Animator>().SetTrigger("OpenDoor");
        }
    }

    protected override void OnCantMove<T>(T component) {
        Wall hitwall = component as Wall;
        hitwall.Damagewall(wallDamage);
        animator.SetTrigger("playerChop");
    }

    private void Restart() {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void loseFood(int loss) {
        animator.SetTrigger("playerHit");
        food -= loss;
        checkIfGameOver();
    }

    private void checkIfGameOver() {
		if (!GetComponent<Health>().IsAlive())
            GameManager.instance.GameOver();
    }
}
