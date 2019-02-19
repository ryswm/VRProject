using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    private Transform button;

    private Vector3 controlPos, locControlPos;

    [Header("SetUp")]
    public float buttonRange;
    public float buttonPressPos;
    public float breakDistance = 0.5f;

    private float originalX;
    private float limit;



    private void Start() {
        button = this.transform;
        originalX = button.position.x;
    }

    private void Update() {
        float currPos = buttonPressPos;
       
        if (attachedController != null) {
            controlPos = attachedController.transform.position;
            
            if (Vector3.Distance(controlPos, button.position) > breakDistance) {     // If controller is too far from button, detach
                attachedController.input.TriggerHapticPulse(5000);
                DetachController();

                return;
            }

            locControlPos = this.transform.InverseTransformPoint(controlPos);
            limit = Mathf.Clamp(locControlPos.x, -buttonRange, buttonRange);


           // currPos = Mathf.Lerp(currPos, currPos+limit, Time.deltaTime);
           // currPos += limit;
            Debug.Log(limit);

        } else {
            currPos = Mathf.Lerp(buttonPressPos, originalX, Time.deltaTime);
        }

        buttonPressPos = currPos;
        Vector3 oldPos = button.localPosition;
        oldPos.x = buttonPressPos;
        button.localPosition = oldPos;



    }
}
