using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour {

	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	public void Update () {
		while (transform.localScale.x < 2.0)
			transform.localScale += new Vector3 (0.2f, 0.2f, 0.2f) * Time.deltaTime;
		if (transform.localScale.x > 2.0f) {
			rb.velocity = transform.TransformDirection (Vector3.forward * 10);
		}
	}
}
