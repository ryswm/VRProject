using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {

    public GameObject laser;    // Laser model
    private GameObject copy;    //Laser copy
    private LineRenderer beam;  //Laser renderer

    public GameObject telePointer;  //Teleport point model
    private GameObject copyPoint;   //Teleport point copy

    private GameObject player;         //Player
    public GameObject cam;              // Camera

    RaycastHit point;


    private bool canTele;                   //If teleport function should be called

    private FadeController fadeEffect;

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
		foreach (string i in Input.GetJoystickNames())
        {
            print(i);
        }

        copy = Instantiate(laser) as GameObject;
        copy.transform.SetParent(this.transform);
        copy.transform.localPosition = Vector3.zero;

        copyPoint = Instantiate(telePointer) as GameObject;
        copyPoint.transform.SetParent(copy.transform);
        copyPoint.SetActive(false);

        beam = copy.GetComponent<LineRenderer>();
        beam.enabled = false;

        player = GameObject.Find("[CameraRig]");
        cam = GameObject.Find("Camera (eye)");

        fadeEffect = GameObject.Find("FadeEffect").GetComponent<FadeController>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {

            beam.enabled = true;
           

            Ray line = new Ray(this.transform.position, this.transform.forward);
           

            beam.SetPosition(0, line.origin);

            if (Physics.Raycast(line, out point, 100))              //If raycast hits
            {
                beam.SetPosition(1, point.point);
                if (point.transform.tag == "Floor")//If raycast hits floor
                {
                    canTele = true;

                    copyPoint.transform.position = point.point;
                    copyPoint.SetActive(true);
                }
                else                             //If not hitting floor
                {
                    canTele = false;
                    copyPoint.SetActive(false);
                }
            }
            else                                                  //If raycast does not hit
            {
                beam.SetPosition(1, line.GetPoint(100));
            }
        } else {                                                                    //If button not pushed

            beam.enabled = false;
            copyPoint.SetActive(false);

            if(canTele)
            {
                
                Teleport();
                
            }
        }
	}

    void Teleport()
    {
        fadeEffect.FadeEffect();

        canTele = false;                //Resetting Variable
        Vector3 diff = player.transform.position - cam.transform.position;      //Keeping y position
        diff.y = 0;
        player.transform.position = point.point + diff;                     // Moving

        fadeEffect.OnFadeReturn();
    }


}
