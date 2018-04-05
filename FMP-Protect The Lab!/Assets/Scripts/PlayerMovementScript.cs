using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    public float PlayerMovementSpeed = 250f;
    public float PlayerRotationSpeed = 250.0f;

    public Vector3 PlayerDirection = Vector3.zero;
    public Vector3 MousePosition;
    public Vector3 RotatePlayerTo;

	// Use this for initialization
	void Start ()
    {
        PlayerMovementSpeed = 2.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckForMovementInput();
    }

    void CheckForMovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(PlayerMovementSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-PlayerMovementSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * PlayerRotationSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * PlayerRotationSpeed);
        }
    }
}
