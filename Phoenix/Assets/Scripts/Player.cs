﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingObject {

    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;
    bool hasKey = false;

    private Animator animator;
	private Image key;
    private int food;

	float idleTime = 2f;
	float idleTimer = 0f;

	// Use this for initialization
	protected override void Start () {
		key = GameObject.Find("HUD_Key").GetComponent<Image>();
		key.color = Color.black;

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

        float horizontal = 0;
        float vertical = 0;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

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
            if(horizontal != 0 && horizontal > 0)
            {
                horizontal += 1 - horizontal;
            }
            else if(horizontal != 0 && horizontal < 0)
            {
                horizontal += -1 - horizontal;
            }
            if (vertical != 0 && vertical > 0)
            {
                vertical += 1 - vertical;
            }
            else if (vertical != 0 && vertical < 0)
            {
                vertical += -1 - vertical;
            }
            idleTimer = 0f;
			AttemptMove<Wall>((int)horizontal, (int)vertical);
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
            GameManager.level++;
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
			key.color = Color.white;
            hasKey = true;
            other.gameObject.SetActive(false);
			GameObject.FindGameObjectWithTag("Exit").GetComponent<Animator>().SetTrigger("OpenDoor");
			GameObject.FindGameObjectWithTag("HUD_Key").SetActive(true);
        }
    }

    protected override void OnCantMove<T>(T component) {
        //Wall hitwall = component as Wall;
        //hitwall.Damagewall(wallDamage);
        //animator.SetTrigger("playerChop");
    }

    private void Restart() {
        print(GameManager.level);
        Application.LoadLevel(Application.loadedLevel);
    }

    public void loseFood(int loss) {
        animator.SetTrigger("playerHit");
        food -= loss;
        checkIfGameOver();
    }

    private void checkIfGameOver() {
        if (!GetComponent<Health>().IsAlive()) {
            GameManager.instance.GameOver();
            enabled = false;
        }
    }

	public void ResetIdleTimer() {
		idleTimer = 0f;
	}
}
