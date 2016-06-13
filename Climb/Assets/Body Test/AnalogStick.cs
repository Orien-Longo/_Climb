using UnityEngine;
using System.Collections;

public class AnalogStick : MonoBehaviour {

    public Rigidbody rightHRB;
    public Rigidbody leftHRB;
    public Rigidbody rightFRB;
    public Rigidbody leftFRB;


    public float force;

    Vector3 rightStick;
    Vector3 leftStick;


    void Start()
    {
        force = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        rightStick = new Vector3(Input.GetAxisRaw("HRStick"), Input.GetAxisRaw("VRStick"), 0);
        leftStick = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        if (Input.GetButton("RightShoulderButton"))
        {
            rightHRB.useGravity = false;
            rightHRB.constraints = RigidbodyConstraints.FreezeAll;
            
        }
        else
        {
            rightHRB.useGravity = true;
            rightHRB.constraints = RigidbodyConstraints.None;
            rightHRB.constraints = RigidbodyConstraints.FreezeRotationY;
            rightHRB.constraints = RigidbodyConstraints.FreezeRotationX;
            rightHRB.constraints = RigidbodyConstraints.FreezePositionZ;
            rightHRB.AddForce(transform.position + (rightStick * force) / Time.smoothDeltaTime);
        }

        if (Input.GetButton("LeftShoulderButton"))
        {
            leftHRB.useGravity = false;
            leftHRB.constraints = RigidbodyConstraints.FreezeAll;

        }
        else
        {
            leftHRB.useGravity = true;
            leftHRB.constraints = RigidbodyConstraints.None;
            leftHRB.constraints = RigidbodyConstraints.FreezePositionZ;
            leftHRB.constraints = RigidbodyConstraints.FreezeRotationY;
            leftHRB.constraints = RigidbodyConstraints.FreezeRotationX;
            leftHRB.AddForce(transform.position + (leftStick * force) / Time.smoothDeltaTime);
        }

        if (Input.GetAxis("RightTrigger") >0.1f)
        {
            rightFRB.useGravity = false;
            rightFRB.constraints = RigidbodyConstraints.FreezeAll;

        }
        else
        {
            rightFRB.useGravity = true;
            rightFRB.constraints = RigidbodyConstraints.None;
            rightFRB.constraints = RigidbodyConstraints.FreezeRotationY;
            rightFRB.constraints = RigidbodyConstraints.FreezeRotationX;
            rightFRB.constraints = RigidbodyConstraints.FreezePositionZ;
            rightFRB.AddForce(transform.position + (rightStick * force) / Time.smoothDeltaTime);
        }

        if (Input.GetAxis("LeftTrigger") > 0.1f)
        {
            leftFRB.useGravity = false;
            leftFRB.constraints = RigidbodyConstraints.FreezeAll;

        }
        else
        {
            leftFRB.useGravity = true;
            leftFRB.constraints = RigidbodyConstraints.None;
            leftFRB.constraints = RigidbodyConstraints.FreezePositionZ;
            leftFRB.constraints = RigidbodyConstraints.FreezeRotationY;
            leftFRB.constraints = RigidbodyConstraints.FreezeRotationX;
            leftFRB.AddForce(transform.position + (leftStick * force) / Time.smoothDeltaTime);
        }
    }
}
