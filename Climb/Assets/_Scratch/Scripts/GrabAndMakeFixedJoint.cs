using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndMakeFixedJoint : MonoBehaviour {

    bool handGrab;
    public GameObject[] knuckleJoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (handGrab)
        {
            Grab();
        }
	}

    void Grab()
    {
        //knuckleJoint[] knuckleJoints = 
        //foreach()
    }
}
