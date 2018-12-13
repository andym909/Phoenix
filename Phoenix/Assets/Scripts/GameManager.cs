using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardCreator boardScript;
    public int playerFoodPoints = 100;

	public static int level = 1;

	// Use this for initialization
	void Start () {
		level = PlayerPrefs.GetInt("Level");

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardCreator>();
		InitGame();
	}

	void Update() {
		
	}

    public void InitGame() {
        BoardCreator.level = GameManager.level;
        setEnemyHealth();
        setEnemyDamage();
        boardScript.SetupScene();
    }

    public void GameOver() {
        GameManager.level = 1;
        Invoke("Restart", 3.5f);
        enabled = false;
    }

    public void setEnemyHealth() {
        boardScript.enemy1.GetComponent<Health>().setStartingHealth((int)Mathf.Floor(3 * Mathf.Log(level) + 3));
        boardScript.enemy2.GetComponent<Health>().setStartingHealth((int)Mathf.Floor(3 * Mathf.Log(level) + 3));
    }

    public void setEnemyDamage() {
        boardScript.enemy1.GetComponent<ChaseAttack>().setDamage(level / 3 + 1);
        boardScript.enemy2.GetComponent<ShootingAttack>().setDamage(level / 3 + 1);
    }

    public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);	
    }

}
