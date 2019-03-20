using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour {

	public int band;
	public float startScale = 0;
	public float scaleMulti = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.freqBands[band] * scaleMulti) + startScale, transform.localScale.z );
		transform.localPosition = new Vector3(transform.localPosition.x, (AudioPeer.freqBands[band] * scaleMulti) + startScale, transform.localPosition.z );
	}
}
