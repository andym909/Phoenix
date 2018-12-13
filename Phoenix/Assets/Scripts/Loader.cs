using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 	Creates gameManager instance
 */ 

public class Loader : MonoBehaviour {

    public GameObject gameManager;		// reference to gameManager
	// Use this for initialization
	void Awake () {
        if (GameManager.instance == null)
            Instantiate(gameManager);
	}

}
