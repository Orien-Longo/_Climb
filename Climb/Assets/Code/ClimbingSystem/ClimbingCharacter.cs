using UnityEngine;
using System.Collections;

public class ClimbingCharacter : MonoBehaviour {

    Vector3 move;

    public bool climb;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (climb)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
      
        move = transform.position;

        move.y += Input.GetAxis("Vertical") * .03f;
        move.x += Input.GetAxis("Horizontal") * .03f;


        transform.position = move;
	
	}
}
