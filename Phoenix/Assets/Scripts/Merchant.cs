using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : MonoBehaviour {

	GameObject player;
	Animator anim;
	Text exchange;
	Player p;
	Health hp;
	HealthDisplay hd;

	public float distance;
	bool withinDistance = false;
	bool merchantOpen = false;

	// Use this for initialization
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

		if(Input.GetKeyDown(KeyCode.X) && exchange.enabled == true && hd.curHealth < hd.maxHealth) {
			if(p.GetFeathers() > 0) {
				p.changeFeathers(-1);
				hp.GainHealth(1);
			}
		}
	}

	public void ShowExchangeText() {
		merchantOpen = true;
		if(withinDistance) {
			exchange.enabled = true;
		}
	}
}
