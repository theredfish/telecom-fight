using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	private GameManager instance = null;
	private int level = 0;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad (gameObject);
		StartGame ();
	}

	void LoadScene() {
		
	}

	void StartGame() {
		// instantiate prefabs and first scene
	}

	public void QuitGame() {
		// todo
	}
}
