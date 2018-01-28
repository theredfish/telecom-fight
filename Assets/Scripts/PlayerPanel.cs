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

	void Awake() {
		Debug.Log (player);
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
	}

	public bool HasControllerAssigned() {
		return this.hasControllerAssigned;
	}
}
