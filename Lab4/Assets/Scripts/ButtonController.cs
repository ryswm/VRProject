using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : Interactable {

    public float buttonRange;
    public float buttonPressPos;

    private void Start() {
        buttonPressPos = this.transform.localPosition.x;
    }
}
