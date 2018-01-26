using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	private float _gravity = 1.0f;
	private CharacterController controller;
	private Vector2 movement = Vector2.zero;
	private bool isForward = true;
	private bool isBackward = false;

	[Header("La puissance du saut")]
	public float jumpForce = 5f;

	[Header("Vitesse de déplacement horizontal")]
	public float moveSpeed = 5f;


	// Use this for initialization
	void Start () {
		this.controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		// First we get the horizontal movement
		movement.x = Input.GetAxis("Horizontal") * this.moveSpeed;

		// Then we get the vertical movement
		if (controller.isGrounded) {
			movement.y = 0;

			// Jump
			if (Input.GetButton("Jump") && this.controller.isGrounded) {
				movement.y = jumpForce;
			}

		} else {
			movement.y -= _gravity;
		}

		WalkOrIdle (movement);
	}

	void WalkOrIdle(Vector2 movement) {
		this.controller.Move(movement * Time.deltaTime);
		float axis = Input.GetAxisRaw("Horizontal");

		if (axis < 0) {
			if (isForward) {
				transform.Rotate(new Vector2(0, -180));
				isForward = false;
				isBackward = true;
			}

		} else if (axis > 0) {
			if (isBackward) {
				transform.Rotate (new Vector3 (0, 180));
				isForward = true;
				isBackward = false;
			}
		} else {
			// idle animation
		}
	}

}
