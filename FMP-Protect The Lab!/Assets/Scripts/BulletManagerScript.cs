using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManagerScript : MonoBehaviour {

    private float RemoveBulletTimer = 5.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.activeInHierarchy == true)
        {
            RemoveBulletTimer -= Time.deltaTime;
        }
        
        if (RemoveBulletTimer <= 0.0f)
        {
            RemoveBulletTimer = 10.0f;
            gameObject.SetActive(false);
        }
	}
}
