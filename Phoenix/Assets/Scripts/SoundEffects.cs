using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {

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

	AudioSource source;
	AudioSource source2;

	// Use this for initialization
	void Awake () {
		source = GetComponent<AudioSource>();
	}

	void Start() {
		source2 = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
	}

	public void PlayNecroDeath() {
		source.clip = necroDeath;
		source.Play();
	}

	public void PlayNecroLoad() {
		source.clip = necroLoad[Random.Range(0, necroLoad.Length-1)];
		source.Play();
	}

	public void PlayNecroStart() {
		source.clip = necroStart;
		source.Play();
	}

	public void PlayNecroStartAgain() {
		source.clip = necroStartAgain;
		source.Play();
	}

	public void PlayPlayerHurt() {
		source2.clip = playerHurt[Random.Range(0, playerHurt.Length-1)];
		source2.Play();
	}

	public void PlayPlayerMelee() {
		source2.clip = playerMelee;
		source2.Play();
	}

	public void PlayPlayerDeath() {
		source2.clip = playerDeath;
		source2.Play();
		PlayerPrefs.SetInt("Start", -1);
	}

	public void PlayFireballShoot() {
		source.clip = fireballShoot;
		source.Play();
	}

	public void PlayEnemyHurt() {
		source.clip = enemyHurt[Random.Range(0, enemyHurt.Length-1)];
		source.Play();
	}

	public void PlayEnemyDeath() {
		source.clip = enemyDeath;
		source.Play();
	}
}
