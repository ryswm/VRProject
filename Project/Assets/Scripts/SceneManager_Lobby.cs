using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneManager_Lobby : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if(col.CompareTag("Player")){
			if(this.transform.CompareTag("LiquidTele")){
				SceneManager.LoadScene("Liquid");
			} else if(this.transform.CompareTag("MusicTele")){
				SceneManager.LoadScene ("Music");
		}
				
	}
}
}
