using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class XylophoneKeyTone : MonoBehaviour {

public double freq = 440.0;
public float gain;

private double increment;
private double phase;
private double sample = 48000.0;

public GameObject wave;
private Renderer waveProp;

void Start(){
	waveProp = wave.GetComponent<Renderer>();
}



void OnAudioFilterRead(float[] data, int channels){
	increment = freq * 2.0 * Mathf.PI / sample;

	for(int i = 0; i < data.Length; i += channels){
		phase += increment;
		data[i] = (float) (gain * Mathf.Sin((float)phase));

		if(channels == 2){
			data[i + 1] = data[i];
		}

		if(phase > (Mathf.PI * 2)){
			phase = 0.0;
		}
	}
}

void OnTriggerEnter(Collider col){
	if(col.GetComponent<Collider>().tag == "Mallet"){
		gain = 0.4f;
		waveProp.material.SetFloat("_FreqCount", waveProp.material.GetFloat("_FreqCount") + 1);
		waveProp.material.SetFloat("_MySpeed", waveProp.material.GetFloat("_MySpeed") + 1);

	}
}

void OnTriggerExit(Collider col){
	if(col.GetComponent<Collider>().tag == "Mallet"){
		gain = 0.0f;
	}
}

}
