using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

	// render the walls
	void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

}
