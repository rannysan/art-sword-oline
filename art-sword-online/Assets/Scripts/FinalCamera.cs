using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCamera : MonoBehaviour {

	public bool lockCursor;
	public float mouseSensitivity;
	public Transform target;
    private Transform target2;
    public float dsFromTarget = 2;
	public Vector2 pitchMinMax = new Vector2 (-40, 85);

	public float rotationSmoothTime = 0.12f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float yaw;
	float pitch;

    int zoom = 5;
    int normal = 90;
    float smooth = 5;

    private bool isZoomed = false;

	void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isZoomed = !isZoomed;
        }

        if (isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
            //target.transform.position += Vector3.right * Time.deltaTime;
            transform.position = target2.position - transform.forward * dsFromTarget;
            mouseSensitivity = 1;
        }

        else
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
            mouseSensitivity = 3;
        }
        
    }

    void LateUpdate () {
		yaw += Input.GetAxis ("RightStickVertical") * mouseSensitivity;
		pitch -= Input.GetAxis ("RightStickHorizontal") * mouseSensitivity;
		yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;

		pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		//Vector3 targetRotation = new Vector3 (pitch, yaw);
		transform.eulerAngles = currentRotation;

        if (isZoomed == false)
        {
            transform.position = target.position - transform.forward * dsFromTarget;
        }
    }
}
