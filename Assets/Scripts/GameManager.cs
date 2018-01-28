using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {
	private GameManager instance = null;
	private int level = 0;
	private PlayerPanel[] playerPanels;
	private List<PlayerController> players;
	private string[] areneScenes;
	private string lastLoadedScene = "Arene1";

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad (gameObject);

		areneScenes [0] = "Arene1";
		areneScenes [1] = "Arene2";

		players = new List<PlayerController> ();

		// Listen scenes loaded
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
		lastLoadedScene = scene.name;
		foreach (PlayerController player in players) {
			Instantiate (player);
		}
		Debug.Log("The secene is loaded, now we need to instantiate players");
	}

	public void StartGame() {
		playerPanels = FindObjectsOfType<PlayerPanel> ().ToArray ();

		// find panels with an assigned controller
		// set players attached to these panels
		for (int i = 0; i < playerPanels.Length; i++) {
			PlayerPanel playerPanel = playerPanels [i];
			PlayerController player = playerPanel.player;
			Debug.Log ("player panel value : " + playerPanel.player);

			if (playerPanel.HasControllerAssigned() && !this.players.Contains(player)) {
				this.players.Add (player);
			}
		}

		// load the first scene
		// TODO : change the first scene name
		LoadScene("Arene1");
	}

	public void Retry() {
		LoadScene (lastLoadedScene);
	}

	public void NextLevel() {
		if (lastLoadedScene == "Arene1") {
			LoadScene (areneScenes[1]);
		}
	}

	public void QuitGame() {
		Application.Quit();
	}
}
