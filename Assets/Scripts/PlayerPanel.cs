using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {
	private bool hasControllerAssigned = false;

	[Header("The panel number to sort it")]
	public int number;

	[Header("The player attached to this panel")]
	public PlayerController player;

	[Header("The player skin")]
	public Image playerSkin;

	[Header("The start button to notify when a player is selected")]
	public StartButton startButton;

	void Awake() {
		playerSkin.enabled = false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/**
	 * Assign the controller number to the panel.
	 * This method will set the player ID which is
	 * used to know the controller (like HorizontalP1).
	 */
	public void AssignController(int controller) {
		Debug.Log ("This player will be the number " + controller);
		this.hasControllerAssigned = true;
		player.id = controller;
		this.playerSkin.enabled = true;

		startButton.CountNewPlayer ();
	}

	public bool HasControllerAssigned() {
		return this.hasControllerAssigned;
	}
}
