using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShieldPosSync : NetworkBehaviour {
    /*
     * 
     * 
     * This Script is unused
     * 
     * 
     */ 


	[SyncVar(hook = "ApplyChange")]
	Transform pos;

	// Use this for initialization
	void Start () {
		pos = this.transform.parent.transform;
	}
	
	// Update is called once per frame
	void Update () {
		pos = this.transform.parent.transform;
	}

	void ApplyChange(Transform change){
		
		this.transform.position = change.position;
		this.transform.rotation = change.rotation;
		this.transform.localScale = change.localScale;
		pos = change;
	}
}
