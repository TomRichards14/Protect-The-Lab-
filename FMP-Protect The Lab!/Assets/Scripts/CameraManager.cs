using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public PlayerManagerScript PlayerReference;

	// Use this for initialization
	void Start ()
    {
        PlayerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagerScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerReference.transform.position.x, transform.position.y, PlayerReference.transform.position.z), 2.0f);
	}
}
