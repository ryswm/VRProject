﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisioncheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
    }

    void OnCollisionEnter(Collision col) {
        Debug.Log("COL WITH"+col.gameObject.name);
      }
}
