using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObject.index); } }
    private void Awake() { trackedObject = GetComponent<SteamVR_TrackedObject>(); }

    private GameObject hitObject;
    private GameObject inHand;
    private FixedJoint joint;

    private void OnCollisionEnter(Collision other)
    {
        if (hitObject)
        {
            return;
        }

        hitObject = other.gameObject;
    }

    private void OnCollisionStay(Collision other)
    {
        if (hitObject)
        {
            return;
        }

        hitObject = other.gameObject;
    }

    private void OnCollisionExit(Collision other)
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

       
    }

    private void Release()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
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