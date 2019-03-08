using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShieldPosSync : NetworkBehaviour {
    /*
     * 
     * 
     * This Script is used
     * 
     * 
     */ 


	[SyncVar]
	public NetworkInstanceId parentID;




	// Use this for initialization
	public override void OnStartClient () {
		GameObject parent = ClientScene.FindLocalObject (parentID);
		transform.SetParent (parent.transform);
    }

}
