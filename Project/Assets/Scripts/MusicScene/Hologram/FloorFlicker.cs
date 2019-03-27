using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFlicker : MonoBehaviour {

    public float glitchChance = .3f;
    private float axis;

    private Renderer holoRend;
    private WaitForSeconds glitchLoopWait = new WaitForSeconds(.1f);
    private WaitForSeconds glitchDur = new WaitForSeconds(.1f);
    public float scaleMulti = 10;
    void Awake(){
        holoRend = GetComponent<Renderer>();
    }

    IEnumerator Start(){
        while(true){
            float glitchTest = Random.Range(0f,1f);
            if(glitchTest <= glitchChance){
                axis = (AudioPeer.freqBands[1] * scaleMulti);
                StartCoroutine (Glitch(axis));
            }
            yield return glitchLoopWait;
        }
    }

    IEnumerator Glitch(float axis){
        glitchDur = new WaitForSeconds(Random.Range(.05f,.25f));
        holoRend.material.SetFloat("_Axis", axis);
        holoRend.material.SetFloat("_Amount", 1f);
        holoRend.material.SetFloat("_CutoutThresh", .29f);
        holoRend.material.SetFloat("_Amplitude", Random.Range(10,20));
        holoRend.material.SetFloat("_Speed", Random.Range(1,10));
        yield return glitchDur;
        holoRend.material.SetFloat("_Amount", 0f);
        holoRend.material.SetFloat("_CutoutThresh", 0f);
    }
}