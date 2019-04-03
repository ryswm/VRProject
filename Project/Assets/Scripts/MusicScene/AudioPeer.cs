using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour {

    private static int numOfBars = 512;
    public static float[] freqBands = new float[8];
    public static AudioSource _audioSource;
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

    public static void changeAudio(Vector2 axis)
    {
        //low2 + (value - low1) * (high2 - low2) / (high1 - low1)
        
        _audioSource.pitch = -3 + (axis.x - -1) * (3 - -3) / (1 - -1);
        if(_audioSource.pitch == 0) _audioSource.pitch = 1;
        _audioSource.spatialBlend = axis.y;
        Debug.Log("PITCH LEVEL: " +  _audioSource.pitch);
         Debug.Log("spatialBlend LEVEL: " +  _audioSource.spatialBlend);
    }


     public static void resetAudio()
    {
        _audioSource.pitch = 1;
        _audioSource.spatialBlend = 0;
        _audioSource.reverbZoneMix = 1;
        Debug.Log("DOPPLER LEVEL: " +  _audioSource.dopplerLevel);

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
