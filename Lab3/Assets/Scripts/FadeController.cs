using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour {

	private Animator animate;

	void Start(){
		animate = GetComponent<Animator>();
	}

	public void FadeEffect(){
		animate.SetTrigger("FadeOut");
	}

	public void OnFadeReturn(){
		animate.SetTrigger("FadeIn");
	}
}
