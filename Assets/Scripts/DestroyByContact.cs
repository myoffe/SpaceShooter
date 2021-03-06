﻿using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start() {
		GameObject gameControllerObj = GameObject.FindWithTag("GameController");
		if (gameControllerObj != null) {
			gameController = gameControllerObj.GetComponent<GameController>();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") return;

		Instantiate(explosion, transform.position, transform.rotation);

		if (other.tag == "Player") {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}

		gameController.AddScore(scoreValue);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
