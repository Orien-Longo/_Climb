using UnityEngine;
using System.Collections;

public class AnalogStick : MonoBehaviour {

    public Transform R_HandTarget;
    public Transform L_HandTarget;
    public Transform R_FootTarget;
    public Transform L_FootTarget;
    public GameObject R_Hand;
    public GameObject L_Hand;
    public GameObject R_Foot;
    public GameObject L_Foot;


    //public float force;

    Vector3 rightStick;
    Vector3 leftStick;


    void Start()
    {
        //force = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        rightStick = new Vector3(Input.GetAxisRaw("HRStick"), Input.GetAxisRaw("VRStick"), 0);
        leftStick = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);


        if (Input.GetButton("RightShoulderButton"))
        {
           
        }
        else
        {
            //R_HandTarget.position += (rightStick * force) / Time.smoothDeltaTime);
        }

        if (Input.GetButton("LeftShoulderButton"))
        {

        }
        else
        {
           // L_HandTarget.position = Vector3.Lerp( leftStick / Time.smoothDeltaTime);
        }

        //if (Input.GetAxis("RightTrigger") >0.1f)
        //{
        //    R_FootTarget.useGravity = false;
        //    R_FootTarget.constraints = RigidbodyConstraints.FreezeAll;

        //}
        //else
        //{
        //    R_FootTarget.useGravity = true;
        //    R_FootTarget.constraints = RigidbodyConstraints.None;
        //    R_FootTarget.constraints = RigidbodyConstraints.FreezeRotationY;
        //    R_FootTarget.constraints = RigidbodyConstraints.FreezeRotationX;
        //    R_FootTarget.constraints = RigidbodyConstraints.FreezePositionZ;
        //    R_FootTarget.AddForce(transform.position + (rightStick * force) / Time.smoothDeltaTime);
        //}

        //if (Input.GetAxis("LeftTrigger") > 0.1f)
        //{
        //    L_FootTarget.useGravity = false;
        //    L_FootTarget.constraints = RigidbodyConstraints.FreezeAll;

        //}
        //else
        //{
        //    L_FootTarget.useGravity = true;
        //    L_FootTarget.constraints = RigidbodyConstraints.None;
        //    L_FootTarget.constraints = RigidbodyConstraints.FreezePositionZ;
        //    L_FootTarget.constraints = RigidbodyConstraints.FreezeRotationY;
        //    L_FootTarget.constraints = RigidbodyConstraints.FreezeRotationX;
        //    L_FootTarget.AddForce(transform.position + (leftStick * force) / Time.smoothDeltaTime);
        //}
    }
}
