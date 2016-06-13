using UnityEngine;
using System.Collections;

public class IK_Snap : MonoBehaviour
{

    Rigidbody rb;

    public bool useIK;

    public bool leftHandIK;
    public bool rightHandIK;

    public bool leftFootIK;
    public bool rightFootIK;

    public Vector3 leftHandPos;
    public Vector3 rightHandPos;

    public Vector3 leftHandOriginalPos;
    public Vector3 rightHandOriginalPos;

    public Vector3 leftFootPos;
    public Vector3 rightFootPos;

    public Vector3 leftHandOffset;
    public Vector3 rightHandOffset;

    public Vector3 leftFootOffset;
    public Vector3 rightFootOffset;

    public Quaternion leftHandRot;
    public Quaternion rightHandRot;

    public Quaternion leftFootRot;
    public Quaternion rightFootRot;

    public Quaternion leftFootRotOffset;
    public Quaternion rightFootRotOffset;

    private Animator anim;

    public Vector3 drawfrom;
    public Vector3 dirL;
    public Vector3 dirR;
    public Vector3 drawfromLF;
    public Vector3 drawfromRF;

    //Joystick Offsets
    public Vector3 rStick;
    public Vector3 lStick;
    public float rStickZ;
    public float lStickZ;

    public bool rStickIsMoving;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        //drawfrom = transform.position + new Vector3(0.0f, 2.0f, 0.5f);
        //dirL = -transform.up + new Vector3(-0.5f, 0.0f, 0.0f);
        //dirR = -transform.up + new Vector3(0.5f, 0.0f, 0.0f);
        //drawfromLF = transform.position + new Vector3(-0.5f, 0.25f, 0f);
        //drawfromRF = transform.position + new Vector3(0.5f, 0.25f, 0f);
        rb = GetComponent<Rigidbody>();



    }

    void Update()
    {


        UserInput();


        if (Input.GetAxisRaw("HRStick") >= 0.1f || Input.GetAxisRaw("HRStick") <= -0.1f || Input.GetAxisRaw("VRStick") >= 0.1f || Input.GetAxisRaw("VRStick") <= -0.1f)
        {
            rStickIsMoving = true;
        }
        else
        {
            rStickIsMoving = false;
        }

        //Left Hand IK Visual Ray
        Debug.DrawRay(drawfrom, dirL, Color.green);

        //Right Hand IK Visual Ray
        Debug.DrawRay(drawfrom, dirR, Color.red);

        //Left Foot IK Visual Ray
        Debug.DrawRay(drawfromLF, transform.forward, Color.yellow);

        //Right Foot IK Visual Ray
        Debug.DrawRay(drawfromRF, transform.forward, Color.blue);
    }

    void FixedUpdate()
    {
        drawfrom = transform.position + new Vector3(0.0f, 2.0f, 0.5f);
        dirL = -transform.up + new Vector3(-0.5f, 0.0f, 0.0f);
        dirR = -transform.up + new Vector3(0.5f, 0.0f, 0.0f);
        drawfromLF = transform.position + new Vector3(-0.5f, 0.25f, 0f);
        drawfromRF = transform.position + new Vector3(0.5f, 0.25f, 0f);
        rStickZ = 1;
        IKCheck();
    }

    void UserInput()
    {
        rStick = new Vector3(Input.GetAxisRaw("HRStick"), Input.GetAxisRaw("VRStick"), (rStickZ * 2f) * Time.deltaTime);
        lStick = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Debug.Log(rStick);

        if ((Input.GetButtonDown("LeftShoulderButton") || Input.GetButtonDown("RightShoulderButton")))
        {

            useIK = true;
            rb.useGravity = false;
            rb.isKinematic = true;
            ////Disables Movement Script
            //transform.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;
            anim.Play("Idle");

            if (Input.GetButtonDown("LeftShoulderButton"))
            {

                leftHandIK = true;
            }
            if (Input.GetButtonUp("LeftShoulderButton"))
            {
                leftHandIK = false;
            }
            if (Input.GetButtonDown("RightShoulderButton"))
            {

                rightHandIK = true;
            }
            if (Input.GetButtonUp("RightShoulderButton"))
            {
                rightHandIK = false;
            }

        }

        else if (!Input.GetButton("LeftShoulderButton") && !Input.GetButton("RightShoulderButton"))
        {

            leftHandIK = false;

            rightHandIK = false;

            useIK = false;

            rb.useGravity = true;
            rb.isKinematic = false;
            //Enables Movement Script
            transform.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;

        }

        //Checks Right Analog Stick
        if (rStickIsMoving)
        {
            rightHandIK = true;
        }

    }

    void IKCheck()
    {
        RaycastHit LHit;
        RaycastHit RHit;

        RaycastHit LFHit;
        RaycastHit RFHit;


        //LeftHandIKCheck
        if (Physics.Raycast(transform.position + transform.TransformDirection(drawfrom), transform.TransformDirection(dirL), out LHit, 1f) && leftHandIK)
        {
            Vector3 lookAt = Vector3.Cross(-LHit.normal, transform.right);
            lookAt = lookAt.y < 0 ? -lookAt : lookAt;

            leftHandPos = LHit.point - transform.TransformDirection(leftHandOffset);
            //leftHandPos.x = leftHandOriginalPos.x - leftHandOffset.x;
            //leftHandPos.z = leftFootPos.z - leftHandOffset.z;
            leftHandRot = Quaternion.LookRotation(LHit.point + lookAt, LHit.normal);

        }
        //else
        //{
        //    leftHandIK = false;
        //}

        //RightHandIKCheck
        if (Physics.Raycast(transform.position + transform.TransformDirection(drawfrom), transform.TransformDirection(dirR), out RHit, 1f) && rightHandIK)
        {
            Vector3 lookAt = Vector3.Cross(-RHit.normal, transform.right);
            lookAt = lookAt.y < 0 ? -lookAt : lookAt;
            rightHandOriginalPos = anim.GetIKPosition(AvatarIKGoal.RightHand);

            if (rStickIsMoving && !Input.GetButtonDown("RightShoulderButton"))
            {

                rightHandPos = rStick;
            }
            if (Input.GetButtonDown("RightShoulderButton") && !rStickIsMoving)
            {
                rightHandPos = (RHit.point - transform.TransformDirection(rightHandOffset)) + rStick;
            }

            //rightHandPos.x = rightHandOriginalPos.x - rightHandOffset.x;
            //rightHandPos.z = rightFootPos.z - rightHandOffset.z;
            rightHandRot = Quaternion.LookRotation(RHit.point + lookAt, RHit.normal);
        }


        //else
        //{
        //    rightHandIK = false;
        //}
        if ((Input.GetButtonDown("LeftShoulderButton") || Input.GetButtonDown("RightShoulderButton")))
        {

            //LeftFootIKCheck
            if (Physics.Raycast(transform.position + transform.TransformDirection(drawfromLF), transform.forward, out LFHit, 1f))
            {
                leftFootIK = true;
                leftFootPos = LFHit.point - leftFootOffset;
                leftFootRot = (Quaternion.FromToRotation(Vector3.up, LFHit.normal)) * leftFootRotOffset;
            }
            else
            {
                leftFootIK = false;
            }

            //RightFootIKCheck
            if (Physics.Raycast(transform.position + transform.TransformDirection(drawfromRF), transform.forward, out RFHit, 1f))
            {
                rightFootIK = true;
                rightFootPos = RFHit.point - rightFootOffset;
                rightFootRot = (Quaternion.FromToRotation(Vector3.up, RFHit.normal)) * rightFootRotOffset;
            }
            else
            {
                rightFootIK = false;
            }
        }
    }

    void OnAnimatorIK()
    {
        //if (useIK)
        //{

        leftHandOriginalPos = anim.GetIKPosition(AvatarIKGoal.LeftHand);
        rightHandOriginalPos = anim.GetIKPosition(AvatarIKGoal.RightHand);

        if (leftHandIK)
        {

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos);

            anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRot);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        }

        if (rightHandIK)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandPos);

            anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandRot);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        }

        if (leftFootIK)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);

            anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
        }

        if (rightFootIK)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);

            anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
        }
        // }
    }

}
