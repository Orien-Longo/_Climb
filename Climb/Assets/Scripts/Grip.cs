using UnityEngine;
using System.Collections;

public class Grip : MonoBehaviour
{

    public GameObject handHold, hand;
    bool grabbing, colliding;
    public float gripMeter, thirdAxis, rTrigger, lTrigger;

    void Start()
    {
        gripMeter = 10f;
        grabbing = false;
        gameObject.GetComponent<Collider>().enabled = false;


    }


    void Update()
    {
        //GripMeter();

        HandCheck();

        Debug.Log(gripMeter);
        //Debug.Log(rTrigger);
        //Debug.Log(lTrigger);
        Debug.Log(thirdAxis);

    }

    void HandCheck()
    {

        //if (CompareTag("RightHand"))
        //{
            thirdAxis = Input.GetAxis("Right Trigger");

        if (thirdAxis > 0 )
        {
            if (gripMeter > 0)
            {
                grabbing = true;
                gripMeter -= 0.01f;
            }
        }
        else if (thirdAxis == 0 || gripMeter <= 0)
        {
            grabbing = false;
        }
        //}
        //else if (CompareTag("LeftHand"))
        //{
        //    thirdAxis = Input.GetAxis("Right Trigger");
        //    if (thirdAxis < 0 && gripMeter > 0)
        //    {
        //        grabbing = true;
        //    }
        //    else
        //    {
        //        grabbing = false;
        //    }
        //}
    }

    void OnTriggerEnter(Collider other/*, Collider[] children*/)
    {
        
        if (other.gameObject.CompareTag("HandHoldArea") && grabbing)
        {
            GetComponent<Collider>().enabled = true;
            //children = other.GetComponentsInChildren<Collider>();
            //foreach (Collider child in children)
            //{
            //    child.GetComponent<Collider>().enabled = true;
            //}
            //colliding = true;
        }
        
    }

    void OnTriggerExit(Collider other/*, Collider[] children*/)
    {
        //if (other.gameObject.CompareTag("HandHoldArea") || !grabbing)
        //{
        //    other.GetComponentInChildren<Collider>().enabled = false;
        //    colliding = false;
        //}
        if (other.gameObject.CompareTag("HandHoldArea") || !grabbing)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    //void GripMeter()
    //{
    //    rTrigger = Input.GetAxis("Right Trigger");
    //    lTrigger = Input.GetAxis("Left Trigger");
    //    if (gripMeter > 0 && grabbing)
    //        gripMeter -= 0.01f;
    //}
}
