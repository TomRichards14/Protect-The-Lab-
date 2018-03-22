using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    public float PlayerMovementSpeed = 250f;
    public float PlayerRotationSpeed = 1.0f;

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
        PlayerRotation();
    }

    void CheckForMovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            PlayerDirection = Vector3.forward;
            PlayerMovement();
        }
    }

    void PlayerMovement()
    {
        transform.position += PlayerDirection * PlayerMovementSpeed * Time.deltaTime;
    }

    void PlayerRotation()
    {
        MousePosition = Input.mousePosition;
        Debug.Log(MousePosition);
        RotatePlayerTo = new Vector3(0.0f, MousePosition.y, 0.0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, RotatePlayerTo, PlayerRotationSpeed);
    }
}
