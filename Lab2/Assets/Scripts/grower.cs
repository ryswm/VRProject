using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grower : MonoBehaviour {
	private Rigidbody rb;
	public float rate;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")){
			Vector3 from = rb.transform.localScale;
            Vector3 to = new Vector3(2.0f,2.0f,2.0f);

            float timer = 0.1f;

			//while(timer <= 1.0f){
				//timer += Time.deltaTime;
                transform.localScale = Vector3.Lerp(from, to, timer*Time.deltaTime);
			//}		
		}
	}


	void blowBalloon(Rigidbody rb){
            Vector3 from = rb.transform.localScale;
            Vector3 to = new Vector3(2.0f,2.0f,2.0f);

            float timer = 0.1f;

			while(timer <= 1.0f){
				timer += Time.deltaTime/rate;
                rb.transform.localScale = Vector3.Lerp(from, to, timer*Time.deltaTime);
			}		

        }
}
