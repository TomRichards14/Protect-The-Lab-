using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    float PlayerSpeed;

    Vector3 PlayerDirection;

	// Use this for initialization
	void Start ()
    {
        PlayerSpeed = 2.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKey(KeyCode.W))
        {
            PlayerDirection = Vector3.forward;
            PlayerMovement();
        }
	}

    void PlayerMovement()
    {
        transform.position = PlayerDirection * PlayerSpeed;
    }
}
