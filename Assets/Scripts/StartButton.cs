using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
	private int countSelectedPlayers = 0;
	private Button btn;

	// Use this for initialization
	void Awake () {
		btn = gameObject.GetComponent<Button> ();
		btn.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (countSelectedPlayers > 1) {
			btn.enabled = true;
		} else {
			btn.enabled = false;
		}
	}

	public void CountNewPlayer() {
		countSelectedPlayers++;
	}
}
