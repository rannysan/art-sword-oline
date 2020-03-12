using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour {

	public float InputX;
	public float InputZ;
	public Vector3 desiredMoveDirection;
	public bool blockRotationPlayer;
	public float desiredRotationSpeed;
	public Animator anim;
	public float Speed;
	public float allowPlayerRotation;
	private Camera cam;
	public CharacterController controller;
	public bool isGrounded;
	private float verticalVel;
	private Vector3 moveVector;
    public float gravity;

    private Vector3 moveDirection = Vector3.zero;


    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController> ();

	}

    void Update () {
		InputMagnitude ();

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection.y -= gravity * Time.deltaTime;

        if (Physics.Raycast(transform.position, -Vector3.up, 1))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

		moveVector = new Vector3 (0, verticalVel, 0);
		controller.Move (moveVector);

	}



    void PlayerMoveAndRotation(){
		InputX = Input.GetAxis ("Horizontal");
		InputZ = Input.GetAxis ("Vertical");

			var camera = Camera.main;
			var forward = cam.transform.forward;
			var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize ();
		right.Normalize ();

		desiredMoveDirection = forward * InputZ + right * InputX;

		if (blockRotationPlayer == false) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
		}
	}

	void InputMagnitude() {
		//calculate Input Vectors
		InputX = Input.GetAxis("Horizontal");
		InputZ = Input.GetAxis("Vertical");

		anim.SetFloat ("InputZ", InputZ, 0.0f, Time.deltaTime * 2f);
		anim.SetFloat ("InputX", InputX, 0.0f, Time.deltaTime * 2f);

		//Calculate the Input Magnitude
		Speed = new Vector2(InputX, InputZ).sqrMagnitude;

		//Phisically move Player
		if (Speed > allowPlayerRotation) {
			anim.SetFloat ("InputMagnitude", Speed, 0.0f, Time.deltaTime);
			PlayerMoveAndRotation ();
		} else if (Speed < allowPlayerRotation) {
			anim.SetFloat ("InputMagnitude", Speed, 0.0f, Time.deltaTime);
		}
	}
}