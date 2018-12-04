using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingObject {

    public float restartLevelDelay = 1f;
    bool hasKey = false;

    private Animator animator;
	private Image key;
	private Text featherText;
    private int feathers = 0;

	float idleTime = 2f;
	float idleTimer = 0f;

	// Use this for initialization
	protected override void Start () {
		featherText = GameObject.Find("Feather_Text").GetComponent<Text>();

		key = GameObject.Find("HUD_Key").GetComponent<Image>();
		key.color = Color.black;

		GetComponent<Health>().SetCanBeDamaged(true);

        animator = GetComponent<Animator>();

        base.Start();
	}

    // Update is called once per frame
    void Update () {

        float horizontal = 0;
        float vertical = 0;

		if(Camera.main.GetComponent<LoadingScreen>().loading == false) {
			horizontal = Input.GetAxisRaw("Horizontal");
			vertical = Input.GetAxisRaw("Vertical");
		}

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
        base.AttemptMove<T>(xDir, yDir);
        checkIfGameOver();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Exit" && hasKey == true) {
			Camera.main.GetComponent<LoadingScreen>().LoadScreen();
			GetComponent<Health>().SetCanBeDamaged(false);
            GameManager.level++;
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Feather") {
			changeFeathers(1);
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "Key")
        {
			key.color = Color.white;
            hasKey = true;
            other.gameObject.SetActive(false);
			GameObject.FindGameObjectWithTag("Exit").GetComponent<Animator>().SetTrigger("OpenDoor");
        }
    }

    protected override void OnCantMove<T>(T component) {
        
    }

    private void Restart() {
        Application.LoadLevel(Application.loadedLevel);
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

	private void changeFeathers(int count) {
		feathers++;
		featherText.text = "x" + feathers.ToString();
	}
}
