using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerToControllerAssigner : MonoBehaviour {
	private List<int> assignedControllers = new List<int>();
	private PlayerPanel[] playerPanels;

	void Awake() {
		playerPanels = FindObjectsOfType<PlayerPanel> ().OrderBy(t => t.number).ToArray ();
	}

	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {
		for (int i = 1; i <= 4; i++) {
			if (assignedControllers.Contains (i)) {
				continue;
			}

			if (Input.GetButtonDown("JumpP" + i)) {
				AddPlayerController (i);
				break;
			}

		}
	}

	void AddPlayerController(int controller) {
		assignedControllers.Add (controller);

		// Assign the controller to the first free panel
		foreach (PlayerPanel playerPanel in playerPanels) {
			if (!playerPanel.HasControllerAssigned()) {
				playerPanel.AssignController (controller);
				break;
			}
		}
	}
}
