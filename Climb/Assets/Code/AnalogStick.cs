using UnityEngine;
using System.Collections;

public class AnalogStick : MonoBehaviour
{

    public Transform R_HandTarget;
    public Transform R_HandTargetStartPos;
    public Transform R_Shoulder;
    public Transform L_HandTarget;
    public Transform R_FootTarget;
    public Transform L_FootTarget;
    public GameObject R_Hand;
    public GameObject L_Hand;
    public GameObject R_Foot;
    public GameObject L_Foot;

    public float distance = 10f;
    public float speed = .5f;
    Vector3 move;
    public float rhx;
    public float rhy;

    float maxLimbDist = 1;
    float deadZone = 0.1f;
    float deltaIKSpeed = 3f;

    bool rStickIsMoving;
    bool inPosition;

    //public float force;

    Vector3 rightStick;
    Vector3 leftStick;


    void Awake()
    {
        inPosition = false;
        rStickIsMoving = false;
        //force = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rightStick = new Vector3(Input.GetAxisRaw("HRStick"), Input.GetAxisRaw("VRStick"), 0);
        leftStick = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        RightHandMovement();
        Debug.Log(Input.GetAxisRaw("HRStick"));
        Debug.Log(Input.GetAxisRaw("VRStick"));


        if ((Input.GetButton("RightShoulderButton")))
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

    void RightHandMovement()
    {



        // If using InputMapper and PS4 controller make sure you are getting inputs from Joystick 1



        if (Input.GetAxisRaw("HRStick") >= deadZone || Input.GetAxisRaw("HRStick") <= -deadZone || Input.GetAxisRaw("VRStick") >= deadZone || Input.GetAxisRaw("VRStick") <= -deadZone)
        {
            IK.rightHandIkActive = true;
            rStickIsMoving = true;
            Transform startPos = R_HandTarget;
            if (!inPosition)
            {
                R_HandTarget.position = Vector3.Lerp(R_HandTarget.position, R_HandTargetStartPos.position, deltaIKSpeed * Time.deltaTime);
                R_HandTarget.rotation = Quaternion.Slerp(R_HandTarget.rotation, R_HandTargetStartPos.rotation, deltaIKSpeed * Time.deltaTime);
                inPosition = true;
            }
        }
        else
        {

            rStickIsMoving = false;
            inPosition = true;
        }



        if (rStickIsMoving && inPosition)
        {

            RightHandMove();


        }
        if (!rStickIsMoving)
        {


            R_HandTarget.position = Vector3.Lerp(R_HandTarget.position, R_Hand.transform.position, deltaIKSpeed * Time.deltaTime);
            R_HandTarget.rotation = Quaternion.Slerp(R_HandTarget.rotation, R_Hand.transform.rotation, deltaIKSpeed * Time.deltaTime);
            inPosition = false;
            IK.rightHandIkActive = false;

        }
    }

    void RightHandMove()
    {
        
        move = R_HandTargetStartPos.position;
        move.x = rhx + rightStick.x * distance;
        move.y = rhy + rightStick.y * distance;
        
        R_HandTarget.position = Vector3.Lerp(R_Shoulder.position, move, Time.deltaTime * deltaIKSpeed);
    }
    
}
