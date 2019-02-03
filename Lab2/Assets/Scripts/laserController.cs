using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserController : MonoBehaviour {

	private LineRenderer laser;
	public RaycastHit hit;


	void Start () {
		laser = GetComponent<LineRenderer> ();
		laser.enabled = true;

	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (transform.parent.position, transform.parent.forward);
		RaycastHit hit;
		laser.SetPosition (0, ray.origin);

		if(Physics.Raycast(ray, out hit, 20)){
			laser.SetPosition(1, hit.point);

			if (hit.rigidbody) {
				Destroy (hit.transform.gameObject);
			}

		} else {
			laser.SetPosition (1, ray.GetPoint (20));
		}
	}
}
