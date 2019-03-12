using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneManager_Lobby : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		Debug.Log("Hu");
		if(col.CompareTag("Player")){
			SceneManager.LoadScene("Music");
		}
	}
}
