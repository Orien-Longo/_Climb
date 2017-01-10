// Written with help from Rafi Alam. :)

using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{

    GameObject ends;
    float dist;
    GameObject[] hold;
    bool snapped;
    public float snapDist;

    public bool rHand;
    static public Vector3 rPos;
    static public Vector3 lPos;
    public float deadzone = .01f;
    public Vector3 deadzoneVector;

    public GameObject skeletonHandR;
    public GameObject skeletonHandL;


    //right analog sticks that will add to the tranform position
    Vector3 rightStick;
    Vector3 leftStick;

    void Update()
    {
        Snap();
        DeadOffset();
        lPos = Vector3.Lerp(lPos, transform.position + leftStick, Time.deltaTime);
        rPos = Vector3.Lerp(rPos, transform.position + rightStick, Time.deltaTime);
    }

    void Snap()
    {
        ends = ClosestHold();

        rightStick = new Vector3(Input.GetAxisRaw("HRStick"), Input.GetAxisRaw("VRStick"), 0);
        leftStick = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        if (ends != null)
        {
            dist = Vector3.Distance(ends.transform.position, transform.position);

            if (Mathf.Abs(dist) < snapDist)
            {
                snapped = true;
                transform.position = ends.transform.position;
                transform.rotation = ends.transform.rotation;
            }
            else
            {
                if (rHand)
                {
                    if (rightStick != Vector3.zero)
                        rPos = Vector3.zero + rightStick;
                }
                else
                {
                    lPos = Vector3.zero;
                }
                snapped = false;
            }
        }
    }

    GameObject ClosestHold()
    {
        hold = GameObject.FindGameObjectsWithTag("HandHold");

        //assign to look for the closest
        GameObject closest = null;
        float distance = 100;
        foreach (GameObject search in hold)
        {
            Vector3 dif = search.transform.position - transform.position;
            float currentDist = dif.sqrMagnitude;

            if (currentDist < distance)
            {
                if (rHand)
                {
                    if (lPos != search.transform.position && Input.GetButton("RightShoulderButton"))
                    {
                        rPos = Vector3.Lerp(rPos, search.transform.position, Time.deltaTime);

                        closest = search;
                        distance = currentDist;
                    }
                }

                else 
                {
                    if (!rHand && rPos != search.transform.position && Input.GetButton("LeftShoulderButton"))
                    {
                        lPos = Vector3.Lerp(lPos, search.transform.position, Time.deltaTime);

                        closest = search;
                        distance = currentDist;
                    }
                    
                }
                
            }
        }
        return closest;
    }

    void DeadOffset()
    {
        if (rightStick.x > 0)
        {
            deadzoneVector.x -= deadzone;
        }
        else
        {
            deadzoneVector.x += deadzone;
        }
        if (rightStick.y > 0)
        {
            deadzoneVector.y -= deadzone;
        }
        else
        {
            deadzoneVector.y += deadzone;
        }

    }
}
