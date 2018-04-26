﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainGameUIScript : MonoBehaviour {

    public Text mainGameHealthText;
    public PlayerManagerScript PlayerReference;

	// Use this for initialization
	void Start ()
    {
        PlayerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManagerScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        mainGameHealthText.text = "Health: " + PlayerReference.CurrentHealth;
	}
}