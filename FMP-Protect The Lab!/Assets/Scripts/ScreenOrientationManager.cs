using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientationManager : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 1;
        Screen.orientation = ScreenOrientation.Landscape;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
