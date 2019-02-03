using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour {
	private Rigidbody rb;
	public float strength;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
	Vector3 from = transform.localScale;
        Vector3 to = new Vector3(2.0f,2.0f,2.0f);
        float timer = 0.1f;

        transform.localScale = Vector3.Lerp(from, to, timer*Time.deltaTime);
		Debug.Log (transform.localScale);

		if (transform.localScale.x >= 1.3f) {

			transform.parent = null;
			rb.isKinematic = false;
			rb.AddForce (Vector3.one * strength);
		}

		if (transform.position.y > 30.0f) {
			Destroy (gameObject);
		}
	}


}
