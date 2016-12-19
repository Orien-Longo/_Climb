using UnityEngine;
using System.Collections;

public class CustomIK : MonoBehaviour
{

    //Our bones
    public Transform upperArm;
    public Transform forearm;
    public Transform hand;

    //Our Targets
    public Transform target;
    public Transform elbowTarget;

    public bool IsEnabled;
    public float Weight;

    //Internal variables
    Quaternion upperArmStartRotation;
    Quaternion forearmStartRotation;
    Quaternion handStartRotation;

    Vector3 targetRelativeStartPosition;
    Vector3 elbowTargetRelativeStartPosition;

    GameObject upperArmAxisCorrection;
    GameObject forearmAxisCorrection;
    GameObject handAxisCorrection;

    Vector3 lastUpperArmPosition;
    Vector3 lastTargetPosition;
    Vector3 lastElbowTargetPosition;

    void Start()
    {

        upperArmStartRotation = upperArm.rotation;
        forearmStartRotation = forearm.rotation;
        handStartRotation = hand.rotation;

        elbowTargetRelativeStartPosition = elbowTarget.position - upperArm.position;

        upperArmAxisCorrection = new GameObject("upperArmAxisCorrection");
        forearmAxisCorrection = new GameObject("forearmAxisCorrection");
        handAxisCorrection = new GameObject("handAxisCorrection");

        upperArmAxisCorrection.transform.parent = transform;
        forearmAxisCorrection.transform.parent = upperArmAxisCorrection.transform;
        handAxisCorrection.transform.parent = forearmAxisCorrection.transform;

    }

    void LateUpdate()
    {
        if (IsEnabled)
        {
            CalculateIK();
        }
    }

    void CalculateIK()
    {
        if(target == null)
        {
            targetRelativeStartPosition = Vector3.zero;
            return;
        }

        if(targetRelativeStartPosition == Vector3.zero && target != null)
        {
            targetRelativeStartPosition = target.position - upperArm.position;
        }

        lastUpperArmPosition = upperArm.position;
        lastTargetPosition = target.position;
        lastElbowTargetPosition = elbowTarget.position;

        float upperArmLength = Vector3.Distance(upperArm.position, forearm.position);
        float foreArmLength = Vector3.Distance(forearm.position, hand.position);

        float armLength = upperArmLength + foreArmLength;

        float hypotenuse = upperArmLength;

        float targetDistance = Vector3.Distance(upperArm.position, target.position);

        targetDistance = Mathf.Min(targetDistance, armLength - 0.0001f);

        float adjacent = (hypotenuse * hypotenuse - foreArmLength * foreArmLength + targetDistance*targetDistance /(2 * targetDistance));

        float ikAngle = Mathf.Acos(adjacent / hypotenuse) * Mathf.Rad2Deg;

        Vector3 targetPostition = target.position;
        Vector3 elbowTargetPosition = elbowTarget.position;

        Transform upperArmParent = upperArm.parent;
        Transform forearmParent = forearm.parent;
        Transform handParent = hand.parent;

        Vector3 upperArmScale = upperArm.localScale;
        Vector3 forearmScale = forearm.localScale;
        Vector3 handScale = hand.localScale;

        Vector3 upperArmLocalPosition = upperArm.localPosition;
        Vector3 forearmLocalPosition = forearm.localPosition;
        Vector3 handLocalPosition = hand.localPosition;

        Quaternion upperArmRotation = upperArm.rotation;
        Quaternion forearmRotation = forearm.rotation;
        Quaternion handRotation = hand.rotation;
        Quaternion handLocalRotation = hand.localRotation;

        target.position = targetRelativeStartPosition + upperArm.position;
        elbowTarget.position = elbowTargetRelativeStartPosition + upperArm.position;
        upperArm.rotation = upperArmStartRotation;
        forearm.rotation = forearmStartRotation;
        hand.rotation = handStartRotation;

        transform.position = upperArm.position;

        transform.LookAt(targetPostition, elbowTargetPosition - transform.position);

        upperArmAxisCorrection.transform.position = upperArm.position;
        upperArmAxisCorrection.transform.LookAt(forearm.position, upperArm.up);
        upperArm.parent = upperArmAxisCorrection.transform;

        //forearmAxisCorrection.transform.parent = forearm;
        forearmAxisCorrection.transform.position = forearm.position;
        forearmAxisCorrection.transform.LookAt(hand.position, forearm.up);
        forearm.parent = forearmAxisCorrection.transform;

        handAxisCorrection.transform.parent = hand;
        hand.parent = handAxisCorrection.transform;

        target.position = targetPostition;
        elbowTarget.position = elbowTargetPosition;

        upperArmAxisCorrection.transform.LookAt(target, elbowTarget.position - upperArmAxisCorrection.transform.position);
        upperArmAxisCorrection.transform.localRotation = Quaternion.Euler(upperArmAxisCorrection.transform.localRotation.eulerAngles - new Vector3(ikAngle, 0, 0));
        forearmAxisCorrection.transform.LookAt(target, elbowTarget.position - upperArmAxisCorrection.transform.position);
        handAxisCorrection.transform.rotation = target.rotation;

        upperArm.parent = upperArmParent;
        forearm.parent = forearmParent;
        hand.parent = handParent;
        upperArm.localScale = upperArmScale;
        forearm.localScale = forearmScale;
        hand.localScale = handScale;
        upperArm.localPosition = upperArmLocalPosition;
        forearm.localPosition = forearmLocalPosition;
        hand.localPosition = handLocalPosition;

        Weight = Mathf.Clamp01(Weight);
        upperArm.rotation = Quaternion.Slerp(upperArmRotation, upperArm.rotation, Weight);
        forearm.rotation = Quaternion.Slerp(forearmRotation, forearm.rotation, Weight);
        hand.rotation = target.rotation;

        
    }
}
