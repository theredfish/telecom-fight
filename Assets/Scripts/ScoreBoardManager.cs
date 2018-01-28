using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreBoardManager : MonoBehaviour {
	private PlayerController[] players;
	public Text[] scoreLabels;
	public Transform[] podiumPositions;

	void Awake() {
		// disable UI text score by default
		foreach (Text scoreLabel in scoreLabels) {
			scoreLabel.enabled = false;
		}

		players = FindObjectsOfType<PlayerController> ().ToArray ();
		SortPlayersByScore();

		// Display score
		for(int i = 0; i < players.Length; i++) {
			int p_score = players[i].GetComponent<PlayerScore> ().GetScore();
			int playerNumberLabel = i + 1;
			scoreLabels [i].text = "Joueur " + playerNumberLabel + " : " + p_score + " points";
			scoreLabels [i].enabled = true;
		}

		// Instantiate player to their starting positions
		for(int i = 0; i < players.Length; i++) {
			players [i].gameObject.transform.position = podiumPositions [i].position;
			players[i].SetAlive (false);
		}
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SortPlayersByScore() {
		for (int i = 0; i < players.Length-1; i++) {
			PlayerController current_player = players [i];
			PlayerScore player_score = current_player.GetComponent<PlayerScore> ();
			int current_score = player_score.GetScore();
			int j = i;
			int next_score = players [j+1].GetComponent<PlayerScore> ().GetScore ();


			while (j < players.Length-1 && next_score > current_score) {
				players [j] = players [j+1];
				j++;
			}

			players [j] = current_player;
		}
	}
}
