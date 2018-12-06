using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEffects : MonoBehaviour {

	Player player;
	Animator anim;
	int facing;
	float offset = 0.65f;

	void Start() {
		player = GetComponentInParent<Player>();
		anim = GetComponentInChildren<Animator>();
	}

	void Update() {
		facing = player.GetFacing();
		UpdatePos();
	}

	void UpdatePos() {
		Vector3 parentPos = transform.parent.transform.position;
		switch(facing) {
			case 0:
				transform.position = new Vector3(parentPos.x, parentPos.y + offset);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				break;
			case 1:
				transform.position = new Vector3(parentPos.x + offset, parentPos.y);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
				break;
			case 2:
				transform.position = new Vector3(parentPos.x, parentPos.y - offset);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
				break;
			case 3:
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
