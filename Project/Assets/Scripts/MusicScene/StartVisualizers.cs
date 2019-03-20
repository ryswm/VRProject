using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVisualizers : MonoBehaviour {
    private static int numOfBars = 512;
    public GameObject barPrefab;
    GameObject[] visualBar = new GameObject[numOfBars];
    public float maxScale = 1000000;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < numOfBars; i++)
        {
            GameObject barInstance = (GameObject)Instantiate(barPrefab);
            barInstance.transform.position = this.transform.position;
            barInstance.transform.parent = this.transform;
            barInstance.name = "Bar" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            barInstance.transform.position = new Vector3(0, 10, 35);
            visualBar[i] = barInstance;
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < numOfBars; i++)
        {
            if (visualBar[i] != null)
            {
                visualBar[i].transform.localScale = new Vector3( 0.25f , (AudioPeer._samples[i] * maxScale) +0 ,0.25f  );
                
            }
        }
    }
}
