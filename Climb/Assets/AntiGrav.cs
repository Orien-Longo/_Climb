using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGrav : MonoBehaviour
{

    public float negGravForce, distMultiplier, newGravForce, distFromFeet;
    public Rigidbody head, torso;
    public GameObject LeftFootObj, RightFootObj;
    public Vector3 feetPos, RFoot, LFoot, torsoPos;
    // Use this for initialization
    void Start()
    {
        negGravForce = 400f;

        

        head = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFootDistance();
        head.AddForce(Vector3.up * newGravForce);
    }

    void CheckFootDistance()
    {
        RFoot = RightFootObj.transform.position;
        LFoot = LeftFootObj.transform.position;
        feetPos = new Vector3(((RFoot.x + LFoot.x) / 2), ((RFoot.y + LFoot.y) / 2), ((RFoot.z + LFoot.z) / 2));
        torsoPos = torso.transform.position;
        distFromFeet = Vector3.Distance(torsoPos, feetPos);
        distMultiplier = 2f - distFromFeet;
        newGravForce = negGravForce * distMultiplier; 
    }
}
