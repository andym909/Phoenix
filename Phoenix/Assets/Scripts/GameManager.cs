using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardCreator boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    private int level = 3;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardCreator>();
        InitGame();
	}

    public void InitGame() {
        boardScript.SetupScene();
    }

    public void GameOver() {
        enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
