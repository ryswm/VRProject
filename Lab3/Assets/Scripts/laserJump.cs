using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserJump : MonoBehaviour {

   

    private LineRenderer beam;
    public Transform person;


    // Use this for initialization
    void Start () {
        beam = GetComponent<LineRenderer>();
        beam.enabled = true;

	}
	
	// Update is called once per frame
	void Update () {
        Ray line = new Ray(transform.parent.position, transform.parent.forward);
        RaycastHit point;

        beam.SetPosition(0, line.origin);
        
        if(Physics.Raycast(line, out point, 100))
        {
            beam.SetPosition(1, point.point);
            if(point.transform.tag == "Floor")
            {
                person.transform.position = point.point;
            }
        }
        else
        {
            beam.SetPosition(1, line.GetPoint(100));
        }
	}
}
