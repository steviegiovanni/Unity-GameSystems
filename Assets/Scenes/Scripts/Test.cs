﻿using UnityEngine;
using GameSystems.Entities;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Q))
			EntityManager.Instantiate (1);
	}
}
