using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbMovement : MonoBehaviour
{

    public GameObject LeftHand, RightHand, LeftFoot, RightFoot;
    public Transform LeftStartWithStick, RightStartWithStick;
    bool leftStickIsMoving, rightStickIsMoving, rHandIsGrabbing, lHandIsGrabbing;

    GameObject[] ends;
    float dist;
    GameObject[] hold;
    bool lHSnapped, rHSnapped, lFSnapped, rFSnapped;
    public float snapDist;

    //private bool rHand;
    static public Vector3 rPos, lastRPos, lPos, lastLPos;

    //right analog sticks that will add to the tranform position
    Vector3 rightStick;
    Vector3 leftStick;

    void Update()
    {
        rightStick = new Vector3(Input.GetAxisRaw("HRStick"), Input.GetAxisRaw("VRStick"), 0);
        leftStick = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        CheckSticks();

        Debug.Log(rightStickIsMoving);
        
        Snap();

    }

    void CheckSticks()
    {
        if (rightStick.x > .01f || rightStick.x < -.01f || rightStick.y > .01f || rightStick.y < -.01f)
        {
            rightStickIsMoving = true;
            

        }
        else
        {
            rightStickIsMoving = false;
        }
        if (rightStickIsMoving)
        {
            lastRPos = RightHand.transform.position;
            RightHand.transform.position =  RightStartWithStick.position + rightStick;
        }
        else { RightHand.transform.position = Vector3.Lerp(RightHand.transform.position, lastRPos, Time.deltaTime); }

        if (leftStick.x > .01f || leftStick.x < -.01f || leftStick.y > .01f || leftStick.y < -.01f)
        {
            leftStickIsMoving = true;
        }
        else
        {
            leftStickIsMoving = false;
        }
    }

    void Snap()
    {
        ends[0] = ClosestHoldToRightHand();



        if (ends[0] != null)
        {
            dist = Vector3.Distance(ends[0].transform.position, RightHand.transform.position);

            if (Mathf.Abs(dist) < snapDist)
            {
                rHSnapped = true;
                RightHand.transform.position = Vector3.Lerp(RightHand.transform.position, ends[0].transform.position, Time.deltaTime);
                RightHand.transform.rotation = Quaternion.Slerp(RightHand.transform.rotation, ends[0].transform.rotation, Time.deltaTime);
            }
            else
            {

                if (rPos != Vector3.zero) { rPos = Vector3.zero; }

                rHSnapped = false;
            }
        }
    }

    GameObject ClosestHoldToRightHand()
    {
        GameObject[] hold = GameObject.FindGameObjectsWithTag("HandHold");

        //assign to look for the closest
        GameObject closest = null;
        float distance = 150;
        foreach (GameObject search in hold)
        {
            Vector3 dif = search.transform.position - RightHand.transform.position;
            float currentDist = dif.sqrMagnitude;

            if (currentDist < distance)
            {
                //if (rHand)
                ////{
                //if (lPos != search.transform.position)
                //{
                if (Input.GetButton("RightShoulderButton"))
                {
                    rPos = Vector3.Lerp(rPos, search.transform.position, Time.deltaTime);

                    closest = search;
                    distance = currentDist;
                }
                //}

            }
        }
        return closest;
    }
}
