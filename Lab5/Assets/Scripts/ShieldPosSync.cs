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
		pos.position = this.transform.parent.transform.position;
        pos.rotation = this.transform.parent.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        pos.position = this.transform.parent.transform.position;
        pos.rotation = this.transform.parent.transform.rotation;
    }

	void ApplyChange(Transform change){
		
		this.transform.position = change.position;
		this.transform.rotation = change.rotation;
		pos = change;
	}
}
