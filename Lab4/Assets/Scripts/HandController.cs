using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HandController : MonoBehaviour {

    /*
     *  Controller Setup
     */
    /* 
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)trackedObject.index); } }
    private void Awake() { trackedObject = GetComponent<SteamVR_TrackedObject>(); }*/

    //Setup
    private LineRenderer laser;
    private RaycastHit hitPoint;
    private GameObject selectedObj;
    private Material[] objMats;

    [Header("Highlight Material:")]
    public Material highlight;

    private bool trigPress;

    private void Start() {
        if (GetComponent<VRTK_ControllerEvents>() == null)
            {
                VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample", "VRTK_ControllerEvents", "the same"));
                return;
            }

        //Setup controller event listeners
        GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
        GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);

        gameObject.AddComponent<LineRenderer>();
        laser = GetComponent<LineRenderer>();
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;

        laser.enabled = false;
    }

    void OnTriggerEnter(Collider col){
        Debug.Log(col.gameObject.name);
    }

    void Update() {
        if (trigPress) {
            Debug.Log("pressed");
            laser.enabled = true;

            Ray cast = new Ray(this.transform.position, this.transform.forward);

            laser.SetPosition(0, cast.origin);

            if (Physics.Raycast(cast, out hitPoint, 100)){
                laser.SetPosition(1, hitPoint.point);
                selectedObj = hitPoint.collider.gameObject;
                if(selectedObj.tag == "Selectable") {
                    SetHighlight(selectedObj);
                } else {
                    if(selectedObj) RemoveHighlight(selectedObj);
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
    }


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





// ******************************   VRTK Functions  ****************************** \\
    private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
        {
            VRTK_Logger.Info("Controller on index '" + index + "' " + button + " has been " + action
                    + " with a pressure of " + e.buttonPressure + " / trackpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)");
        }

        private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "pressed", e);
            trigPress = true;
        }

        private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "released", e);
            trigPress = false;
        }
}
