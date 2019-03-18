using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour {

    private static int numOfBars = 512;

    AudioSource _audioSource;
    public static float[] _samples = new float[numOfBars];
	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
       
    }
	
	// Update is called once per frame
	void Update () {
        getSpecturmAudioSource();

    }

    void getSpecturmAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0,FFTWindow.Blackman);
    }
}
