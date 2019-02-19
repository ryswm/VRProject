using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    /*
     *  Controller Setup
     */
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObject.index); } }
    private void Awake() { trackedObject = GetComponent<SteamVR_TrackedObject>(); }

    //Setup
    private LineRenderer laser;
    private RaycastHit hitPoint;
    private GameObject selectedObj;
    private Material[] objMats;
    public Material highlight;

    private void Start() {
        laser = this.GetComponent<LineRenderer>();
        laser.enabled = false;
    }

    void Update() {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip)) {
            laser.enabled = true;

            Ray cast = new Ray(this.transform.position, this.transform.forward);

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
                RemoveHighlight(selectedObj);
                selectedObj = null;
            }
        }
        else {
            laser.enabled = false;
        }
    }


    void SetHighlight(GameObject selectedObj) {
        objMats = selectedObj.GetComponent<Renderer>().materials;
        objMats[1] = highlight;
        selectedObj.GetComponent<Renderer>().materials = objMats;
    }

    void RemoveHighlight(GameObject selectedObj) {
        objMats = selectedObj.GetComponent<Renderer>().materials;
        objMats[1] = null;
        selectedObj.GetComponent<Renderer>().materials = objMats;
    }
}
