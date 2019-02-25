using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badHandController : MonoBehaviour {

    //Controller Setup
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObject.index); } }
    private void Awake() { trackedObject = GetComponent<SteamVR_TrackedObject>(); }

    //Setup
    private LineRenderer laser;
    private RaycastHit hitPoint;

    private FixedJoint joint;

    [Header("Selected Objects:")]
    public GameObject selectedObj;
    private GameObject heldItem;
    private GameObject hitObj;

    
    [Header("Highlight Material:")]
    private Material[] objMats;
    public Material highlight;

    private void Start() {
        laser = this.GetComponent<LineRenderer>();
        laser.enabled = false;

        
    }

    void Update() {

        // Laser pointer on Grip press
        if (Controller.GetHairTrigger()) {
            laser.enabled = true;

            Ray cast = new Ray(trackedObject.transform.position, trackedObject.transform.forward);

            laser.SetPosition(0, cast.origin);

            if (Physics.Raycast(cast, out hitPoint, 100)){
                
                laser.SetPosition(1, hitPoint.point);
                selectedObj = hitPoint.collider.gameObject;
                if(selectedObj.tag == "Selectable") {
                    SetHighlight(selectedObj);
                } else {
                    RemoveHighlight(selectedObj);
                    selectedObj = null;
                }
            }
            else {
                laser.SetPosition(1, cast.GetPoint(100));
                if(selectedObj) RemoveHighlight(selectedObj);
                selectedObj = null;
            }
        }
        else {
            laser.enabled = false;
        }

        if(Controller.GetPress(SteamVR_Controller.ButtonMask.Grip)){
            
            if(hitObj){
                Grab();
            }
        } else {
            if (heldItem) Release();
        }
    }

    //Highlight control
    void SetHighlight(GameObject selectedObj) {
        objMats = selectedObj.GetComponent<Renderer>().materials;
        objMats[1] = highlight;
        selectedObj.GetComponent<Renderer>().materials = objMats;
    }

    void RemoveHighlight(GameObject selectedObj) {
        objMats = selectedObj.GetComponent<Renderer>().materials;
        if(objMats.Length > 1) objMats[1] = null;
        selectedObj.GetComponent<Renderer>().materials = objMats;
    }

    
    //Object interaction
    void Grab(){
        joint = gameObject.AddComponent<FixedJoint>();
        heldItem = hitObj;
        joint.connectedBody = heldItem.GetComponent<Rigidbody>();
        Debug.Log(heldItem.name);
    }

    void Release(){
        if(joint.connectedBody){
            heldItem.GetComponent<Rigidbody>().velocity = Controller.velocity;
            heldItem.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
            joint.connectedBody = null;
            Destroy(joint);
        }
        heldItem = null;
    }

    //Collider functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null) return;
        hitObj = other.gameObject;
        Debug.Log(hitObj.name);
    }

    private void OnTriggerStay(Collider other){
        if(other.attachedRigidbody == null) return;
        hitObj = other.gameObject;
    }

    private void OnTriggerExit(Collider other){
        hitObj = null;
    }

    private void OnCollisionEnter(Collision col){
        Debug.Log(col);
    }

    
}
