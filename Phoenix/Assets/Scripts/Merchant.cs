using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour {

	GameObject player;
	Animator anim;

	public float distance;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, player.transform.position) <= distance) {
			anim.SetTrigger("merchantPop");
		}
	}
}
