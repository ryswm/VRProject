using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonCollider : MonoBehaviour {

	private bool isHit = false;

	private Vector3 startPos, currPos;

	void Start(){
		startPos = GetComponent<Transform>().position;
		currPos =  startPos;
	}

	void Update(){
		if(isHit){
			Debug.Log("Should be Moving");
			currPos.x -= 0.01f;
		}
		else {
			currPos.x = Mathf.Lerp(currPos.x, currPos.x, startPos.x);
		}
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

	void OnTriggerExit(Collider col){
		isHit = false;
	}
}
