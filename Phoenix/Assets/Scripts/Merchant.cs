using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 	Controller for merchant logic and visuals
 * 	
 */ 

public class Merchant : MonoBehaviour {

	GameObject player;	// Reference to the Player
	Animator anim;		// Reference to the merchant animator
	Text exchange;		// Reference to the text prompting the player
	Player p;			// Reference to the player script
	Health hp;			// Reference to the Player Health
	HealthDisplay hd;	// Reference to the HUD Health display

	float distance = 1.5f;  		// Distance at which the merchant and player can interact
	bool withinDistance = false;	// If within that distance
	bool merchantOpen = false;		// If the player has opened the merchant animation

	// Set all those references
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		p = player.GetComponent<Player>();
		hp = player.GetComponent<Health>();
		hd = GameObject.Find("GameManager(Clone)").GetComponent<HealthDisplay>();
		anim = GetComponent<Animator>();
		exchange = GameObject.Find("ExchangeText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		// Set animation and exchange text when the Player is within the distance
		if(Vector3.Distance(transform.position, player.transform.position) <= distance) {
			anim.SetTrigger("merchantPop");
			withinDistance = true;
			if(merchantOpen) {
				ShowExchangeText();
			}
		}
		else {
			withinDistance = false;
			exchange.enabled = false;
		}

		// Exchange a feather for health on correct input
		if(Input.GetButtonDown("Fire2") && exchange.enabled == true && hd.GetCurHealth() < hd.GetMaxHealth()) {
			if(p.GetFeathers() > 0) {
				p.changeFeathers(-1);
				hp.GainHealth(1);
			}
		}
	}

	// Show the exchange text
	public void ShowExchangeText() {
		merchantOpen = true;
		if(withinDistance) {
			exchange.enabled = true;
		}
	}

	// Distance getter
	public float GetDistance() {
		return distance;
	}
}
