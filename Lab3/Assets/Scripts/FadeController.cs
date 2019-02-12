using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour {

	public Animator animate;

	public void FadeEffect(){
		animate.SetTrigger("FadeOut");
	}

	public void OnFadeReturn(){
		animate.SetTrigger("FadeIn");
	}
}
