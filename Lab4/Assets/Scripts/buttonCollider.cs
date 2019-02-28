using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonCollider : MonoBehaviour {

	private bool isHit = false;

	private Vector3 startPos, currPos;
	private Vector3 limit;
	private float t = 0.0001f;

	private Rigidbody rb, rFreeze, rStart;

	private Vector3 force = new Vector3(-0.0001f, 0f, 0f);

	public bool active;
	



	void Start(){
		startPos = GetComponent<Transform>().position;
		limit = new Vector3(startPos.x + 0.06f, startPos.y, startPos.z);

		rb = GetComponent<Rigidbody>();
		rStart = rb;
		rFreeze = rb;

		Debug.Log("Start" + startPos);

	}

	void Update(){
		rb.isKinematic = false;
		currPos = GetComponent<Transform>().position;
		

		Debug.Log(currPos);
		
		if(currPos.x > limit.x && isHit == true){
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			active = true;
		}else{
			rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}
		

		//Mathf.Clamp(currPos.x, startPos.x, limit.x);
		gameObject.transform.position = currPos;
	}

	void FixedUpdate(){
		if(isHit == false && currPos.x > startPos.x){
			rb.AddForce(force);
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "controller"){
			isHit = true;
		}
	}

	void OnTriggerStay(Collider col){
		if(col.gameObject.tag == "controller"){
			isHit = true;
		}
	}

	void OnCollisionExit(Collision col){
		isHit = false;
	}
}
