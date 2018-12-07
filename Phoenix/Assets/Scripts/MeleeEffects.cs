using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEffects : MonoBehaviour {

	Player player;
	Animator anim;
	SpriteRenderer sprite;
	int facing;
	float offset = 0.65f;

	void Start() {
		player = GetComponentInParent<Player>();
		anim = GetComponentInChildren<Animator>();
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	void Update() {
		facing = player.GetFacing();
		UpdatePos();
	}

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

	public void MeleeAttackAnim() {
		anim.SetTrigger("attack");
	}

}
