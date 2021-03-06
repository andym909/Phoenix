﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingObject {

    public float restartLevelDelay = 1f;
    bool hasKey = false;

	private HealthDisplay hd;
    private Animator animator;
	private Image key;
	private Text featherText;
    private int feathers = 0;

	float idleTime = 2f;
	float idleTimer = 0f;

	bool gameEnding = false;

	// Use this for initialization
	protected override void Start () {
        // set up the heads up display
		hd = GameObject.Find("GameManager(Clone)").GetComponent<HealthDisplay>();

		featherText = GameObject.Find("Feather_Text").GetComponent<Text>();
		changeFeathers(PlayerPrefs.GetInt("Feathers"));

		key = GameObject.Find("HUD_Key").GetComponent<Image>();
		key.color = Color.black;

        // turn damage on
		GetComponent<Health>().SetCanBeDamaged(true);

        animator = GetComponent<Animator>();

        base.Start();   // start MovingObject
	}

    // Update is called once per frame
    void Update () {
		if(!gameEnding) {
			checkIfGameOver();

			float horizontal = 0;
			float vertical = 0;

            // set variables if everything is done loading
			if(Camera.main != null && Camera.main.GetComponent<LoadingScreen>().GetLoading() == false) {
				horizontal = Input.GetAxisRaw("Horizontal");
				vertical = Input.GetAxisRaw("Vertical");
			}

            // if horizontal in non-zero, move horizontally
			if(horizontal != 0) {
				animator.SetBool("movement", true);
				vertical = 0;

				if(horizontal > 0) {
					animator.SetInteger("facing", 1);   // facing right
				}
				else {
					animator.SetInteger("facing", 3);   // facing left
				}
			}
			else if(vertical > 0) {                     // moving vertically
				animator.SetBool("movement", true); 
				animator.SetInteger("facing", 0);       // facing up    
			}
			else if(vertical < 0) {
				animator.SetBool("movement", true);
				animator.SetInteger("facing", 2);       // facing down
			}
			else {
				animator.SetBool("movement", false);    // not moving
			}


			if(horizontal != 0 || vertical != 0) {         // Adjusting the input to allow for the controller support
				if(horizontal != 0 && horizontal > 0) {    // to make sure that the program does not ignore an input
					horizontal += 1 - horizontal;          // between zero and one
				}
				else if(horizontal != 0 && horizontal < 0) {
					horizontal += -1 - horizontal;
				}
				if(vertical != 0 && vertical > 0) {
					vertical += 1 - vertical;
				}
				else if(vertical != 0 && vertical < 0) {
					vertical += -1 - vertical;
				}
				idleTimer = 0f;
				AttemptMove<Wall>((int)horizontal, (int)vertical);
			}
			else {
				idleTimer += Time.deltaTime;
				if(idleTimer >= idleTime) {                // Setting the player's animation to the forward idle animation  
					animator.SetInteger("facing", 2);      // if no direction has been pressed within the idleTimer time
				}
				StopMotion();
			}
		}
	}

    protected override void AttemptMove<T>(int xDir, int yDir) {
        base.AttemptMove<T>(xDir, yDir);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Exit" && hasKey == true) {                // If the player has the key and collides with the exit
			SavePrefs();                                            // save the stats, go to the loading screen, and 
			Camera.main.GetComponent<LoadingScreen>().LoadScreen(); // increase the level
			GetComponent<Health>().SetCanBeDamaged(false);
            GameManager.level++;
			PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Feather") { // If the player collides with a feather, increase the feather count
			changeFeathers(1);             // and disable the feather object
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "Key")
        {
			key.color = Color.white;            // If the player colides with the key, set the HUD to display the key
            hasKey = true;                      // Allow the player to exit the level, play the open door animation
            other.gameObject.SetActive(false);  // and disable the key object
			GameObject.FindGameObjectWithTag("Exit").GetComponent<Animator>().SetTrigger("OpenDoor");
        }
        else if(other.tag == "Speed_PowerUp" && this.moveTime <= 18f)
        {
            this.moveTime++;                        // If the player collides with the speed power up, increase the move
			PlayerPrefs.SetInt("Speed", moveTime);  // time to speed the player up, save the speed in Player Prefs, and 
            other.gameObject.SetActive(false);      // disable the orb
			Helper.IncreaseSpeed(this.moveTime);
        }
        else if (other.tag == "Damage_PowerUp") // If the player collides with the damage power up
        {
            CloseHelper closeHelp = GetComponent<PlayerAttackClose>().invisibleProjectile.GetComponent<CloseHelper>();
            closeHelp.increaseDamage(1); // Increase the damage of the close attack
            Helper rangeHelp = GetComponent<PlayerAttackDistance>().projectile.GetComponent<Helper>();
            rangeHelp.increaseDamage(1); // Increase the damage of the ranged attack
            other.gameObject.SetActive(false); // Disable the orb
        }
        else if (other.tag == "Health_PowerUp")
        {
			hd.IncreaseMax(2);                 // If the player collides with the health power up, increase the health
            other.gameObject.SetActive(false); // by two and disable the orb
        }
    }

    protected override void OnCantMove<T>(T component) {
        
    }

    private void Restart() {
        Application.LoadLevel(Application.loadedLevel);
    }

    private void checkIfGameOver() {
        // if we run out of health, kill the player and reset the game
        if (!GetComponent<Health>().IsAlive()) {
			animator.SetTrigger("playerDeath");
            GameManager.instance.GameOver();
			GameManager.level = 1;
			BoardCreator.level = 1;
			Camera.main.GetComponent<LoadingScreen>().SetLoading(true);
			gameEnding = true;
        }
    }

	public void ResetIdleTimer() {
		idleTimer = 0f;
	}

	public int GetFeathers() {
		return feathers;
	}

	public void changeFeathers(int count) {
		feathers += count;
		featherText.text = "x" + feathers.ToString();
	}

    // save the player's stats
	void SavePrefs() {
		PlayerPrefs.SetInt("MaxHealth", hd.GetMaxHealth());
		PlayerPrefs.SetInt("Health", hd.GetCurHealth());
		PlayerPrefs.SetInt("Feathers", feathers);
	}

	public int GetFacing() {
		return animator.GetInteger("facing");
	}
}
