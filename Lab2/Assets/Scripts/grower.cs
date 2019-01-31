using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grower : MonoBehaviour {
	public float rate;

	void Update () {
		Vector3 from = transform.localScale;
        Vector3 to = new Vector3(2.0f,2.0f,2.0f);
        float timer = 0.1f;

        transform.localScale = Vector3.Lerp(from, to, timer*Time.deltaTime);
	}
}
