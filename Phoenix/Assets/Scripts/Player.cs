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



	// Use this for initialization
	protected override void Start () {
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
			vertical = 0;

			if(horizontal > 0) {
				animator.SetInteger("Movement", 1);
			}
			else {
				animator.SetInteger("Movement", 3);
			}
		}
		else if(vertical > 0) {
			animator.SetInteger("Movement", 0);
		}
		else if(vertical < 0) {
			animator.SetInteger("Movement", 2);
		}
		else {
			animator.SetInteger("Movement", -1);
		}

        if (horizontal != 0 || vertical != 0)
            AttemptMove<Wall>(horizontal, vertical);
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
        if (food <= 0)
            GameManager.instance.GameOver();
    }
}
