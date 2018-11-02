﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;

	void Update() {
		if(player != null)
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
	}
}
