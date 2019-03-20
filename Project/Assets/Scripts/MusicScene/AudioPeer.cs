using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour {

    private static int numOfBars = 512;
    public static float[] freqBands = new float[8];
    AudioSource _audioSource;
    public static float[] _samples = new float[numOfBars];
	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
       
    }
	
	// Update is called once per frame
	void Update () {
        getSpecturmAudioSource();
        makeFrequencyBands();
    }

    void getSpecturmAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0,FFTWindow.Blackman);
    }

    void makeFrequencyBands()
    {
        int band = 0;
        for (int i =0; i < 8; i++)
        {
            float average = 0;
            int sampCount = (int)Mathf.Pow(2,i) * 2;
            
            for(int j = 0; j < sampCount; j++)
            {
                average += _samples[band] * (band + 1);
                band++;
            }

            average /= band;
            freqBands[i] = average * 10;
        }
    }

}
