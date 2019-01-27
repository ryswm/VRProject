using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float timerLength;
	public Text timer;
	public GameObject gameObject;
	private Vector3 trig;

	// Use this for initialization
	void Start(){
		trig = gameObject.transform.position;
		timer.text = timerLength.ToString();
	}
	// Update is called once per frame
	void Update () {
		if(!(gameObject.transform.position.Equals(trig))){
			timerLength -= Time.deltaTime;
			timer.text = timerLength.ToString();
			if(timerLength <= 0){
				
			}
		}
		
	}
}
