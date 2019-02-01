using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSpheres : MonoBehaviour {
	public Rigidbody sphere;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")){
			Rigidbody clone;
			clone = Instantiate(sphere);
			clone.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
			clone.transform.parent = null;
		}
	}
}
