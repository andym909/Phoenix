using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 	This script controls the visuals of the Player's melee attack
 * 
 */ 

public class MeleeEffects : MonoBehaviour {

	Player player;			// Reference to the Player script
	Animator anim;			// Reference to the melee attack animator
	SpriteRenderer sprite;	// Reference to the melee sprites
	int facing;				// Direction in which the player is facing
	float offset = 0.65f;	// How far away the graphics will be from the player's center

	// Set References to objects
	void Start() {
		player = GetComponentInParent<Player>();
		anim = GetComponentInChildren<Animator>();
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	// Update the direction the player is facing and position
	void Update() {
		facing = player.GetFacing();
		UpdatePos();
	}

	// Set position and rotation based on player and change sorting layer for visual effect
	void UpdatePos() {
		Vector3 parentPos = transform.parent.transform.position;
		switch(facing) {
			case 0:
				sprite.sortingLayerName = "Items";
				transform.position = new Vector3(parentPos.x, parentPos.y + offset * 1.2f);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				break;
			case 1:
				sprite.sortingLayerName = "Items";
				transform.position = new Vector3(parentPos.x + offset, parentPos.y);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
				break;
			case 2:
				sprite.sortingLayerName = "Units";
				transform.position = new Vector3(parentPos.x, parentPos.y - offset);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
				break;
			case 3:
				sprite.sortingLayerName = "Items";
				transform.position = new Vector3(parentPos.x - offset, parentPos.y);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
				break;
			default:
				break;
		}
	}

	// Trigger the actual animation
	public void MeleeAttackAnim() {
		anim.SetTrigger("attack");
	}

}
