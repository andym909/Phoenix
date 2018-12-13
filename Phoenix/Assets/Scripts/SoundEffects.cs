using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 		Plays every sound effect in the game
 * 
 */ 

public class SoundEffects : MonoBehaviour {


	// References to many, many, different audio clips
	public AudioClip necroStart;
	public AudioClip[] necroLoad;
	public AudioClip necroDeath;
	public AudioClip necroStartAgain;
	public AudioClip[] playerHurt;
	public AudioClip playerMelee;
	public AudioClip playerDeath;
	public AudioClip fireballShoot;
	public AudioClip[] enemyHurt;
	public AudioClip enemyDeath;

	// Audio Source references
	AudioSource source;
	AudioSource source2;

	// Setting the source references
	void Awake () {
		source = GetComponent<AudioSource>();
	}
	void Start() {
		source2 = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
	}

	// Every one of the following methods simply plays the clip that is specified in the method name.
	//		There is also some volume tweaking that occurs.

	public void PlayNecroDeath() {
		source.volume = 1;
		source.clip = necroDeath;
		source.Play();
	}

	public void PlayNecroLoad() {
		source.volume = 1;
		source.clip = necroLoad[Random.Range(0, necroLoad.Length-1)];
		source.Play();
	}

	public void PlayNecroStart() {
		source.volume = 1;
		source.clip = necroStart;
		source.Play();
	}

	public void PlayNecroStartAgain() {
		source.volume = 1;
		source.clip = necroStartAgain;
		source.Play();
	}

	public void PlayPlayerHurt() {
		source2.volume = 1;
		source2.clip = playerHurt[Random.Range(0, playerHurt.Length-1)];
		source2.Play();
	}

	public void PlayPlayerMelee() {
		source2.volume = 0.5f;
		source2.clip = playerMelee;
		source2.Play();
	}

	public void PlayPlayerDeath() {
		source2.volume = 1;
		source2.clip = playerDeath;
		source2.Play();
		PlayerPrefs.SetInt("Start", -1);
	}

	public void PlayFireballShoot() {
		source.volume = 0.3f;
		source.clip = fireballShoot;
		source.Play();
	}

	public void PlayEnemyHurt() {
		source.volume = 1;
		source.clip = enemyHurt[Random.Range(0, enemyHurt.Length-1)];
		source.Play();
	}

	public void PlayEnemyDeath() {
		source.volume = 0.4f;
		source.clip = enemyDeath;
		source.Play();
	}
}
