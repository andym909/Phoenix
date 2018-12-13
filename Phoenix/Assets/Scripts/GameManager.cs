using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 *  GameManager: The script that controls the initialization of the game, increasing the level, and restarting the game
 */
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
        BoardCreator.level = GameManager.level;   // Make sure that the board creator is on the correct level
        setEnemyHealth();                         // Logarithmically increase the enemy health
        setEnemyDamage();                         // Logarithmically increase the enemy damage
        boardScript.SetupScene();                 // Set up the next level for the player to be in
    }

    public void GameOver() {
        GameManager.level = 1;                    // Set the level back to one when the player dies
        Invoke("Restart", 3.5f);                  // Restart the game
        enabled = false;
    }

    //Logarithmically increase the enemy health
    public void setEnemyHealth() {
        boardScript.enemy1.GetComponent<Health>().setStartingHealth((int)Mathf.Floor(3 * Mathf.Log(level) + 3));
        boardScript.enemy2.GetComponent<Health>().setStartingHealth((int)Mathf.Floor(3 * Mathf.Log(level) + 3));
    }

    //Logarithmically increase the enemy damage
    public void setEnemyDamage() {
        boardScript.enemy1.GetComponent<ChaseAttack>().setDamage(level / 3 + 1);
        boardScript.enemy2.GetComponent<ShootingAttack>().setDamage(level / 3 + 1);
    }

    //Go back to the title screen
    public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);	
    }

}
