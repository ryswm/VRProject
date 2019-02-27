using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonCollider : MonoBehaviour {

	private bool isHit = false;

	private Vector3 startPos, currPos;
	private Vector3 limit;
	private float t = 0.0001f;

	void Start(){
		startPos = GetComponent<Transform>().position;
		limit = new Vector3(startPos.x + 0.2f, startPos.y, startPos.z);

		Debug.Log("Start" + startPos);
		Debug.Log("limit" + limit);
	-		
	}

	void Update(){
		currPos = GetComponent<Transform>().position;
		if(isHit){
			
			if(currPos.x > limit.x) currPos.x = limit.x;
		}
		else {
			Debug.Log("Should be Moving");
			currPos = new Vector3(Mathf.Lerp(currPos.x, startPos.x, t), currPos.y, currPos.z);


		
		}
			gameObject.transform.position = currPos;
	}

	void OnCollisionEnter(Collision col){
		Debug.Log("hit");
		if(col.gameObject.tag == "controller"){
			isHit = true;
		}
	}

	void OnTriggerStay(Collider col){
		Debug.Log("Hitting");
		if(col.gameObject.tag == "controller"){
			isHit = true;
		}
	}

	void OnCollisionExit(Collision col){
		Debug.Log("Exit");
		isHit = false;
	}
}
