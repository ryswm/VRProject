using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {

    public GameObject laser;
    private GameObject copy;
    private LineRenderer beam;
    private GameObject cam;

    private SteamVR_TrackedObject trackedObj;


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start () {
		foreach (string i in UnityEngine.Input.GetJoystickNames())
        {
            print(i);
        }

        copy = Instantiate(laser, this.transform ) as GameObject;
        
        beam = copy.GetComponent<LineRenderer>();
        beam.enabled = false;

        cam = GameObject.Find("Camera Rig");
    }
	
	// Update is called once per frame
	void Update () {
        copy.transform.position = this.transform.position;
        beam.enabled = true;
        if (Controller.GetHairTriggerDown())
        {

         
           
            Ray line = new Ray(transform.parent.position, transform.parent.forward);
            RaycastHit point;

            beam.SetPosition(0, line.origin);

            if (Physics.Raycast(line, out point, 100))
            {
                beam.SetPosition(1, point.point);
                if (point.transform.tag == "Floor")
                {
                    cam.transform.position = point.point;
                }
            }
            else
            {
                beam.SetPosition(1, line.GetPoint(100));
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            beam.enabled = false;
        }
	}

    void renderLine()
    {
       
    }
}
