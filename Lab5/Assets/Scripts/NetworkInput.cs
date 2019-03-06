using UnityEngine;

//This object is added to the controller network avatars when the program is running as a client.
[RequireComponent(typeof(Rigidbody), typeof(FixedJoint))]
public class NetworkInput : MonoBehaviour {
    
    public SteamVR_TrackedObject trackedObject;
    public NetworkedCameraRig player;
    GameObject model;
    GameObject shield;

    GameObject obj;
    
    private void Start()
    {
        // Consider why our models are neither meshRenderers on the network avatars, nor child objects of them.
        // How come we have to use joints? 

        model = transform.parent.Find(this.name + "Model").gameObject;

        obj = this.gameObject;
    }

    void Update () {
        if (trackedObject)
        {
            // disable hands if not tracked.
            model.SetActive(trackedObject.isValid);
            if (trackedObject.isValid) {
                // have the net avatars track the steamvr tracked-objects
                transform.position = trackedObject.transform.position;
                transform.rotation = trackedObject.transform.rotation;

                // Send input events to the NetworkedCameraRig instance. (This is our local player)
                var input = SteamVR_Controller.Input((int)trackedObject.index);
                if (input.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
                {
                    // Why does this method have to live in the NetworkedCameraRig class?
                    player.CmdCreateSphere(transform.position + transform.forward, transform.rotation);
                }

                if (input.GetHairTriggerDown()) {   //If Trigger Pulled
                    if (this.transform.childCount > 0) {    //If controller has children
                        if(this.transform.GetChild(0).tag == "Shield") player.CmdDropShield(obj);   //If Child is shield
                    }
                    else {
                        player.CmdCreateShield(trackedObject.transform.position + trackedObject.transform.forward, transform.rotation, obj);
                    }

                }
               
            }


            if (this.transform.childCount > 0) {    //If controller has children
                model.SetActive(false);
            }
        }
    }



    
}
