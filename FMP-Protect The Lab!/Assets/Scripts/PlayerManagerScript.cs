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
        Vector3 MousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
