using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	private Text scoreText;
	private Text restartText;
	private Text gameOverText;
	private int score;

	private bool gameOver = false;
	private bool restart = false;

	void Start() {
		scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
		restartText = GameObject.FindWithTag("RestartText").GetComponent<Text>();
		gameOverText = GameObject.FindWithTag("GameOverText").GetComponent<Text>();

		restartText.text = "";
		gameOverText.text = "";

		score = 0;
		UpdateScore();
		StartCoroutine(SpawnWaves());
	}

	void Update() {
		if (restart && Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);

		while(true) {
			for (int i=0; i<hazardCount; i++) {
				var spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Instantiate(hazard, spawnPosition, Quaternion.identity);
				yield return new WaitForSeconds(spawnWait);
			}

			yield return new WaitForSeconds(waveWait);

			if (gameOver) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore();
	}

	public void GameOver() {
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
