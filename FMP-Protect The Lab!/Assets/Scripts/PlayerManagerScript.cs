using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour {

    public GameObject BulletPrefab;

    private float PlayerMovementSpeed = 5.0f;
    private float PlayerRotationSpeed = 250.0f;
    private float BulletTravelSpeed = 1500.0f;
    public float FireAngle;

    private int QuantityOfBulletsInObjectPool = 25;
    private int MaximumHealth = 1000;
    public int CurrentHealth;
    private int AmmoCapacity = 10;

    private bool HasBulletFired;
    private bool IsPlayerDead;

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
        IsPlayerDead = false;
        CurrentHealth = MaximumHealth;

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
        CheckForMovementInput();
        CorrectingPlayerPosition();

        Debug.Log(AmmoCapacity);

        //If the player has died
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsPlayerDead = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            HasBulletFired = false;
            PlayerFiring();
        }        
    }

    public void CheckForMovementInput()
    {
        //Movement for Android
        //If the phone has a gyroscope
        /*if (Input.gyro.enabled == true)
        {
            transform.position += Input.acceleration * PlayerMovementSpeed * Time.deltaTime;
        }
        //If the phone doesn't have a gyroscope
        else
        {

        }*/


        //Movement for testing on Windows
        ///*
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, PlayerMovementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -PlayerMovementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * PlayerRotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * PlayerRotationSpeed * Time.deltaTime);
        }
        //*/
    }

    public void PlayerFiring()
    {
        //Takes the mouse position and converts it from screen to world

        Ray playerHitsRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(playerHitsRay, out hit, 100);

        Debug.Log(hit.point);

        Vector3 PointToLokkAt = hit.point;
        PointToLokkAt.y = this.transform.position.y;

        transform.LookAt(PointToLokkAt);

        //Goes through the object pool and sets one to active if it's inactive
        for (int i = 0; i < BulletObjectPool.Count; i++)
        {
            if ((!BulletObjectPool[i].activeInHierarchy) && (HasBulletFired == false))
            {
                BulletObjectPool[i].SetActive(true);

                BulletObjectPool[i].transform.position = transform.position;
                BulletObjectPool[i].transform.eulerAngles = new Vector3(90.0f, transform.eulerAngles.y, FireAngle);

                Rigidbody rigidComponent = BulletObjectPool[i].GetComponent<Rigidbody>();
                rigidComponent.AddForce(transform.forward * BulletTravelSpeed);

                HasBulletFired = true;
            }
        }
    }

    public void CorrectingPlayerPosition()
    {
        if ((transform.position.y < 1) || (transform.position.y > 1))
        {
            transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
        }

        if ((transform.rotation.x < 0) || (transform.rotation.x > 0))
        {
            transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, 0.0f);
        }

        if ((transform.rotation.z < 0) || (transform.rotation.z > 0))
        {
            transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, 0.0f);
        }
    }
}
