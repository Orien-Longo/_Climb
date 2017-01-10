using UnityEngine;
using System.Collections;

public class reverseGravity : MonoBehaviour {

    public Rigidbody rb;
    public Transform leftHand;
    public Transform rightHand;
    public Transform leftFoot;
    public Transform rightFoot;

    public float upforce;


	void Start () {
        upforce = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        rb.AddForce(leftHand.position * Time.deltaTime);
        rb.AddForce(rightHand.position * Time.deltaTime);
        rb.AddForce(-leftFoot.position * Time.deltaTime);
        rb.AddForce(-rightFoot.position * Time.deltaTime);
        //rb.AddForce(transform.up * upforce / Time.deltaTime);
    }
}
