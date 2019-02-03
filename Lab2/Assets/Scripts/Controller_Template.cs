using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Template : MonoBehaviour {
    
	public Rigidbody balloon;
	public GameObject laser;
	GameObject pointer;


    private SteamVR_TrackedObject trackedObj;
 
    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    
    // Use this for initialization

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start() {
        foreach (string i in UnityEngine.Input.GetJoystickNames()) {
            print(i);

        }
        
    }
	// Update is called once per frame
	void Update () {
        // If left Grip Button is pressed, Spawn ballon
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
            Debug.Log(gameObject.name + " Grip Press");
			Rigidbody clone;
			clone = Instantiate (balloon, transform.position, transform.rotation) as Rigidbody;
			clone.transform.SetParent (trackedObj.transform);
			clone.transform.localPosition = Vector3.zero;
    
        }
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip)) {
            Debug.Log(gameObject.name + " Grip Press");
    
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
            Debug.Log(gameObject.name + " Grip Release");

        }

        if (Controller.GetAxis() != Vector2.zero) {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }

        
		if (Controller.GetHairTriggerDown ()) {
			pointer = Instantiate (laser) as GameObject;
			pointer.transform.SetParent (trackedObj.transform);
			pointer.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.5f);
		}

        if (Controller.GetHairTrigger()) {
            Debug.Log(gameObject.name + "Press Hold");


        }

        if (Controller.GetHairTriggerUp()) {
            Debug.Log(gameObject.name + " Trigger Release");
			Destroy (pointer);
        }


    }



	/*void newBalloon(GameObject parent){
		
	}*/

}
