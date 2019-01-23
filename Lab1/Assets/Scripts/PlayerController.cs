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
        float jumpUp = Input.GetAxis("Jump");
        Vector3 v3 = new Vector3(-moveVertical,0.0f,moveHorizontal);
        rb.AddForce(v3*speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 8)
        {
            winText.text = "You Win!";
        }
    }
}
