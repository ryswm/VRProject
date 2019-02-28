using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objController : MonoBehaviour {

	public GameObject button1, button2, button3;
	public buttonCollider buttonPush1, buttonPush2, buttonPush3;
	private Material[] objMats;
	public Material highlight;
	// Use this for initialization
	void Start () {


		button1 = GameObject.Find("button1");
		buttonPush1 = button1.GetComponent<buttonCollider>();
		button2 = GameObject.Find("button2");
		buttonPush2 = button2.GetComponent<buttonCollider>();
		button3 = GameObject.Find("button3");
		buttonPush3 = button3.GetComponent<buttonCollider>();

		objMats = this.GetComponent<Renderer>().materials;
	}
	
	// Update is called once per frame
	void Update () {
		if(objMats[1] == highlight){
			if(buttonPush1.active == true){
				Destroy(this.gameObject);
			}
		}
		
	}
}
