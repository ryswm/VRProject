using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Template : MonoBehaviour {

 
    public GameObject Player;
    private Transform lasterVector;
    private Vector3 hitVec;
    private LineRenderer cast;
    private bool hitGround;

    int scale = 10;

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
        cast = GetComponent<LineRenderer>();
        cast.enabled = false;
        hitGround = false;
    }
	// Update is called once per frame
	void Update () {
        cast.enabled = false;
        // If left Grip Button is pressed, Spawn ballon
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
            Debug.Log(gameObject.name + " Grip Press2");
    
        }
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip)) {
            Debug.Log(gameObject.name + " Grip Press1");
            Debug.Log("GRIP AUDIO CHANGE" + Controller.GetAxis().x);
            AudioPeer.changeAudio(Controller.GetAxis());
    
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
            Debug.Log(gameObject.name + " Grip Release");
            AudioPeer.resetAudio();
        }

        if (Controller.GetAxis() != Vector2.zero) {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }

        
        if (Controller.GetHairTriggerDown()) {
            
        }

        if (Controller.GetHairTrigger()) {
            Debug.Log(gameObject.name + "Press Hold");
            Debug.Log(gameObject.name + Controller.GetAxis());
            laserShow();
           
          
        }

        if (Controller.GetHairTriggerUp()) {
            Debug.Log(gameObject.name + " Trigger Release");
            Teleport();
        }
        

    }

    public void laserShow()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        cast.SetPosition(0, ray.origin);
        RaycastHit rayhit;
        cast.enabled = true;
        Debug.Log("Ray Origin 1: " + ray.origin);
        Debug.Log(gameObject.name + "Laser update");
        Debug.Log("Postion Count: " + cast.positionCount);
        if (Physics.Raycast(ray, out rayhit, 1000))
        {
            hitVec = rayhit.point;
            cast.SetPosition(0, this.transform.position);
            cast.SetPosition(1, hitVec);
            Debug.Log("Postion Count: " + cast.positionCount);
            Debug.Log("Ray Origin 2: " + ray.origin);
            Debug.Log("RayCast Point 1: " + rayhit.point);
            if (rayhit.collider.CompareTag("stage"))
            {
                hitGround = true;
                Debug.Log("hitGround " + cast.positionCount);

            }
            else
            {
                hitGround = false;
            }
        }
        else
        {
            cast.SetPosition(0, ray.origin);
            cast.SetPosition(1, ray.GetPoint(100));
            Debug.Log("Postion Count: " + cast.positionCount);
            Debug.Log("Ray Origin 3: " + ray.origin);
            Debug.Log("RayCast Point 2: " + rayhit.point);
            hitGround = false;
        }
    }

    void Teleport()
    {
        if (hitGround)
        {
            Player.transform.position = hitVec;
        }
       
    }

}
