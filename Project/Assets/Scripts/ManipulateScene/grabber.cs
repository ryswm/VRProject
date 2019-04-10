using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabber : MonoBehaviour {

    private GameObject hitObject;
    private GameObject inHand;
    private FixedJoint joint;




    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (hitObject)
        {
            return;
        }

        hitObject = other.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (hitObject)
        {
            return;
        }

        hitObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        hitObject = null;
    }

    private void Grab()
    {
        inHand = hitObject;
        joint = gameObject.AddComponent<FixedJoint>();
        joint.breakForce = 100;
        joint.breakForce = 1000;

        joint.connectedBody = inHand.GetComponent<Rigidbody>();
        inHand.GetComponent<Collider>().isTrigger = true;
        inHand.GetComponent<Rigidbody>().useGravity = false;

       
    }

    private void Release()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            inHand.GetComponent<Collider>().isTrigger = false;
            inHand.GetComponent<Rigidbody>().useGravity = true;
            inHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            inHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        inHand = null;
    }

    private void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            if (hitObject)
            {
                Grab();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            Release();
        }
    }
}
