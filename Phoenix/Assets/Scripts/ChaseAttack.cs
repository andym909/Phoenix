﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAttack : MonoBehaviour {

    public GameObject player;
    public float speed = 2f;
    private float minDistance = 1f;
    private float range;


    // Use this for initialization
    void Awake () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        range = Vector2.Distance(transform.position, player.transform.position);
        if (range > minDistance)
        {
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
	}
}
