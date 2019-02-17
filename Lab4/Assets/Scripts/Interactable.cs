using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour {

    public bool Stealable = true;
    public WandController attachedControl { get; private set; };
}
