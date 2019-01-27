using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
    public int jump;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        count = 0;
        SetCountText();
        winText.text = "";
	}

    void FixedUpdate(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 v3 = new Vector3(-moveVertical,0.0f,moveHorizontal);
        rb.AddForce(v3*speed);

        if(Input.GetKeyDown("space")){
             rb.AddForce(new Vector3(0,jump,0), ForceMode.Impulse);
        }
       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        else if(other.gameObject.CompareTag("Finish")){
            other.gameObject.SetActive(false);
            if(count >= 8){
                winText.text = "You Win!";
            }
            else{
                winText.text = "You didn't get all of the items!!\n You Lose!";
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
