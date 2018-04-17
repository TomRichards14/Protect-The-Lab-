using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour {

    public GameObject BulletPrefab;

    private float PlayerMovementSpeed = 5f;
    private float PlayerRotationSpeed = 250.0f;

    private int QuantityOfBulletsInObjectPool = 25;

    public Vector3 PlayerDirection = Vector3.zero;
    public Vector3 MousePosition;
    public Vector3 RotatePlayerTo;

    List<GameObject> BulletObjectPool;

	// Use this for initialization
	void Start ()
    {
        //Instantiating the object pool for the bullets
        BulletObjectPool = new List<GameObject>();

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
        if (Input.GetButtonDown("Fire1"))
        {
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
        Vector3 MousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Converts it into a usable form for the game
        Vector3 BulletTrajectory = new Vector3(MousePosInWorld.x, 1.0f, MousePosInWorld.z);

        //Goes through the object pool and sets one to active if it's inactive
        for (int i = 0; i < BulletObjectPool.Count; i++)
        {
            if (!BulletObjectPool[i].activeInHierarchy)
            {
                BulletObjectPool[i].SetActive(true);

                BulletObjectPool[i].transform.position = transform.position;
                BulletObjectPool[i].transform.eulerAngles = transform.eulerAngles;
                

                //NEED TO ADD FORCE OR ADD SOMETHING TO MOVE THE BULLET TOWARDS BULLETTRAJECTORY
            }
        }
    }
}
