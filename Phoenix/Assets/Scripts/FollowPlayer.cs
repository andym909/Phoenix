using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 	Simple script to make the Camera follow the Player
 */ 

public class FollowPlayer : MonoBehaviour {

	GameObject player;		// Reference to the player object

	// Update position to match the player's position
	void Update() {
        if (player != null)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

	// Set the player reference
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
