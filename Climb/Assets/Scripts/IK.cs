
using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IK : MonoBehaviour
{

    protected Animator avatar;

    public static bool ikActive = false;
    public static bool rightHandIkActive = false;
    public static bool leftHandIkActive = false;
    public static bool rightFootIkActive = false;
    public static bool leftFootIkActive = false;
    public static bool bodyAndLookIKActive = false;

    public Transform bodyObj = null;
    public Transform leftFootObj = null;
    public Transform rightFootObj = null;
    public Transform leftHandObj = null;
    public Transform rightHandObj = null;
    public Transform lookAtObj = null;

    public float bodyWeightPosition = 0;
    public float bodyWeightRotation = 0;

    public float leftFootWeightPosition = 0;
    public float leftFootWeightRotation = 0;

    public float rightFootWeightPosition = 0;
    public float rightFootWeightRotation = 0;

    public float leftHandWeightPosition = 0;
    public float leftHandWeightRotation = 0;

    public float rightHandWeightPosition = 0;
    public float rightHandWeightRotation = 0;

    public float lookAtWeight = 1.0f;

    // Use this for initialization
    void Start()
    {

        avatar = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (!ikActive)
            {
                ikActive = true;
                ClimbingCharacter.climb = true;
            }
            else
            {
                ikActive = false;
                ClimbingCharacter.climb = false;
            }
        }

        if (ikActive)
        {
            rightHandIkActive = true;
            leftHandIkActive = true;
            rightFootIkActive = true;
            leftFootIkActive = true;
            bodyAndLookIKActive = true;
        }
        else
        {
            rightHandIkActive = false;
            leftHandIkActive = false;
            rightFootIkActive = false;
            leftFootIkActive = false;
            bodyAndLookIKActive = false;
        }
    }

    void OnGUI()
    {

        //GUILayout.Label("Activate IK and move the Effectors around in Scene View");
        ikActive = GUILayout.Toggle(ikActive, "Activate All IK");
        rightHandIkActive = GUILayout.Toggle(rightHandIkActive, "Right Hand IK");
        leftHandIkActive = GUILayout.Toggle(leftHandIkActive, "Left Hand IK");
        rightFootIkActive = GUILayout.Toggle(rightFootIkActive, "Right Foot IK");
        leftFootIkActive = GUILayout.Toggle(leftFootIkActive, "Left Foot IK");
        bodyAndLookIKActive = GUILayout.Toggle(leftFootIkActive, "Body & Look IK");
    }


    void OnAnimatorIK(int layerIndex)
    {
        if (avatar)
        {

            RightHandIK();
            LeftHandIK();
            RightFootIK();
            LeftFootIK();
            BodyLookIK();

        }
    }

    void RightHandIK()
    {
        if (rightHandIkActive)
        {
            avatar.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeightPosition);
            avatar.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeightRotation);

            if (rightHandObj != null)
            {
                //rightHandObj.rotation =  Quaternion.Inverse(Quaternion.Euler( new Vector3(rightHandObj.rotation.x + 180,rightHandObj.rotation.y + 180, rightHandObj.rotation.z+180)));
                avatar.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                avatar.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            }
        }
        else
        {
            avatar.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            avatar.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

            if (rightHandObj != null)
            {
                rightHandObj.position = avatar.GetIKPosition(AvatarIKGoal.RightHand);
                rightHandObj.rotation = avatar.GetIKRotation(AvatarIKGoal.RightHand);
            }
        }
    }

    void LeftHandIK()
    {
        if (leftHandIkActive)
        {
            avatar.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeightPosition);
            avatar.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeightRotation);

            if (leftHandObj != null)
            {
                avatar.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                avatar.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            }
        }
        else
        {
            avatar.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            avatar.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);

            if (leftHandObj != null)
            {
                leftHandObj.position = avatar.GetIKPosition(AvatarIKGoal.LeftHand);
                leftHandObj.rotation = avatar.GetIKRotation(AvatarIKGoal.LeftHand);
            }
        }
    }

    void RightFootIK()
    {
        if (rightFootIkActive)
        {
            avatar.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootWeightPosition);
            avatar.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootWeightRotation);

            if (rightFootObj != null)
            {
                avatar.SetIKPosition(AvatarIKGoal.RightFoot, rightFootObj.position);
                avatar.SetIKRotation(AvatarIKGoal.RightFoot, rightFootObj.rotation);
            }
        }
        else
        {
            avatar.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            avatar.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);

            if (rightFootObj != null)
            {
                rightFootObj.position = avatar.GetIKPosition(AvatarIKGoal.RightFoot);
                rightFootObj.rotation = avatar.GetIKRotation(AvatarIKGoal.RightFoot);
            }
        }
    }

    void LeftFootIK()
    {
        if (leftFootIkActive)
        {
            avatar.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootWeightPosition);
            avatar.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootWeightRotation);

            if (leftFootObj != null)
            {
                avatar.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootObj.position);
                avatar.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootObj.rotation);
            }
        }
        else
        {
            avatar.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
            avatar.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);

            if (leftFootObj != null)
            {
                leftFootObj.position = avatar.GetIKPosition(AvatarIKGoal.LeftFoot);
                leftFootObj.rotation = avatar.GetIKRotation(AvatarIKGoal.LeftFoot);
            }
        }
    }

    void BodyLookIK()
    {
        if (bodyAndLookIKActive)
        {

            // The Look
            avatar.SetLookAtWeight(lookAtWeight, 0.3f, 0.6f, 1.0f, 0.5f);

            if (lookAtObj != null)
            {
                avatar.SetLookAtPosition(lookAtObj.position);
            }

            //The Body
            if (bodyObj != null)
            {
                avatar.bodyPosition = bodyObj.position;
                avatar.bodyRotation = bodyObj.rotation;
            }
        }
        else
        {
            avatar.SetLookAtWeight(0.0f);

            if (lookAtObj != null)
            {
                lookAtObj.position = avatar.bodyPosition + avatar.bodyRotation * new Vector3(0, 0.5f, 1);
            }

            if (bodyObj != null)
            {
                bodyObj.position = avatar.bodyPosition;
                bodyObj.rotation = avatar.bodyRotation;
            }
        }
    }
}
