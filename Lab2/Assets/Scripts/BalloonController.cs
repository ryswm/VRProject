using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour {
	public Rigidbody controller;
	private Vector3 offset;
	void Start(){
		offset = transform.position - controller.transform.position;
	}
	void Update () {
		Vector3 from = rb.transform.localScale;
        Vector3 to = new Vector3(2.0f,2.0f,2.0f);
        float timer = 0.1f;

        transform.localScale = Vector3.Lerp(from, to, timer*Time.deltaTime);
	}

	void LateUpdate () {
        transform.position = controller.transform.position + offset;
	}
}
