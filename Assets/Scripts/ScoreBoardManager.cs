using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardManager : MonoBehaviour {
	public GameObject[] players;
	public Text[] scoreLabels;
	public Transform[] podiumPositions;

	void Awake() {
		// disable UI text score by default
		foreach (Text scoreLabel in scoreLabels) {
			scoreLabel.enabled = false;
		}

		SortPlayersByScore();

		// Display score
		for(int i = 0; i < players.Length; i++) {
			int p_score = players[i].GetComponent<PlayerScore> ().getScore();
			scoreLabels [i].text = p_score.ToString();
			scoreLabels [i].enabled = true;
		}
	}

	void Start () {
		// Instantiate player to their starting positions
		for(int i = 0; i < players.Length; i++) {
			GameObject player = Instantiate (players [i], podiumPositions [i].position, Quaternion.identity);
			player.GetComponent<PlayerController> ().SetAlive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SortPlayersByScore() {
		for (int i = 0; i < players.Length-1; i++) {
			GameObject current_player = players [i];
			int current_score = current_player.GetComponent<PlayerScore>().getScore();
			int j = i;
			int next_score = players [j+1].GetComponent<PlayerScore> ().getScore ();


			while (j < players.Length-1 && next_score > current_score) {
				players [j] = players [j+1];
				j++;
			}

			players [j] = current_player;
		}
	}
}
