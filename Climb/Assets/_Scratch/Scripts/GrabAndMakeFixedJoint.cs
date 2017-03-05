using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndMakeFixedJoint : MonoBehaviour {

	bool LHandGrab, RHandGrab, LFootGrab, RFootGrab;
    public GameObject[] knuckleJoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (LHandGrab)
        {
            LHGrab();
        }
	}

    void LHGrab()
    {
        //knuckleJoint[] knuckleJoints = 
        //foreach()
    }
}
