﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour {

    public GameObject BulletPrefab;

    private float PlayerMovementSpeed = 5.0f;
    private float PlayerRotationSpeed = 250.0f;
    private float BulletTravelSpeed = 5.0f;

    private int QuantityOfBulletsInObjectPool = 25;

    private bool HasBulletFired;

    public Vector3 PlayerDirection = Vector3.zero;
    public Vector3 MousePosition;
    public Vector3 RotatePlayerTo;

    List<GameObject> BulletObjectPool;

	// Use this for initialization
	void Start ()
    {
        //Instantiating the object pool for the bullets
        BulletObjectPool = new List<GameObject>();
        HasBulletFired = false;

        //Adding the GameObjects to the object pool
        for (int i = 0; i < QuantityOfBulletsInObjectPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(BulletPrefab);
            obj.SetActive(false);
            BulletObjectPool.Add(obj);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Debug.Log(Input.mousePosition);

        CheckForMovementInput();
        if (Input.GetButtonDown("Fire1"))
        {
            HasBulletFired = false;
            PlayerShooting();
        }        
    }

    public void CheckForMovementInput()
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
            transform.Rotate(-Vector3.up * PlayerRotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * PlayerRotationSpeed * Time.deltaTime);
        }
    }

    public void PlayerShooting()
    {
        //Takes the mouse position and converts it from screen to world
        Vector3 PlayerShootingDirection = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        PlayerShootingDirection = Camera.main.ScreenToWorldPoint(PlayerShootingDirection) - transform.position;

        Vector3 PlayerLookAtDirection = PlayerShootingDirection - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.up, PlayerLookAtDirection);

        //Goes through the object pool and sets one to active if it's inactive
        for (int i = 0; i < BulletObjectPool.Count; i++)
        {
            if ((!BulletObjectPool[i].activeInHierarchy) && (HasBulletFired == false))
            {
                BulletObjectPool[i].SetActive(true);

                BulletObjectPool[i].transform.position = transform.position;
                BulletObjectPool[i].transform.eulerAngles = new Vector3(90.0f, transform.eulerAngles.y, transform.eulerAngles.z);

                Rigidbody rigidComponent = BulletObjectPool[i].GetComponent<Rigidbody>();
                rigidComponent.velocity = transform.forward * BulletTravelSpeed;

                //Vector3.Slerp(BulletObjectPool[i].transform.position, BulletTrajectory, BulletTravelSpeed);

                HasBulletFired = true;
            }

            transform.LookAt(BulletObjectPool[i].transform);
        }
    }


}