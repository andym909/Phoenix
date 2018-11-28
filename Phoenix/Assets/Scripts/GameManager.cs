using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardCreator boardScript;
    public int playerFoodPoints = 100;

    public static int level = 1;

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
        BoardCreator.level = GameManager.level;
        boardScript.SetupScene();
    }

    public void GameOver() {
        GameManager.level = 1;
        Invoke("Restart", 1f);
        enabled = false;
    }

    public void Restart() {
        print(GameManager.level);
        Application.LoadLevel(Application.loadedLevel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
