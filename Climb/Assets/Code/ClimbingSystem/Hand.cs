// Written with help from Rafi Alam. :)

using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{

	GameObject ends;
	float dist;
	GameObject[] hold;
	bool LSnapped, RSnapped;
	public float snapDist;

	private bool rHand;
	static public Vector3 rPos;
	static public Vector3 lPos;
	//public float deadzone = .01f;
	public Vector3 deadzoneVector;

	public GameObject skeletonHandR;
	public GameObject skeletonHandL;


	//right analog sticks that will add to the tranform position
	Vector3 rightStick;
	Vector3 leftStick;

	void Start ()
	{
//		string handName;
//		handName = gameObject.name;
//		if (handName == "RHandTarget") {
//			rHand = true;
//		} if (handName == "LHandTarget") {
//			rHand = false;
//		}
	}

	void Update ()
	{
		
		Snap ();
		//DeadOffset();
        
		lPos = Vector3.Lerp(lPos, transform.position, Time.deltaTime);
		rPos = Vector3.Lerp(rPos, transform.position, Time.deltaTime);
	}

	void Snap ()
	{
		ends = ClosestHold ();

		rightStick = new Vector3 (Input.GetAxisRaw ("HRStick"), Input.GetAxisRaw ("VRStick"), 0);
		leftStick = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"), 0);

		if (ends != null) {
			dist = Vector3.Distance (ends.transform.position, transform.position);

			if (Mathf.Abs (dist) < snapDist) {
				
				if (rHand) {
					if (Input.GetButton ("RightShoulderButton")) {
						RSnapped = true;
						transform.position = ends.transform.position;
					}
				} else {
					if (Input.GetButton ("LeftShoulderButton")) {
						LSnapped = true;
						transform.position = ends.transform.position;
					}
				}
				transform.rotation = ends.transform.rotation;
			} else {
				if (rHand) {
					if (rPos != Vector3.zero) {
						RSnapped = false;
						rPos = Vector3.zero + rightStick;
					}
				} else {
					if (lPos != Vector3.zero) {
						LSnapped = false;
						lPos = Vector3.zero + leftStick;
					}
				}

			}
		}
	}

	GameObject ClosestHold ()
	{
		hold = GameObject.FindGameObjectsWithTag ("HandHold");

		//assign to look for the closest
		GameObject closest = null;
		float distance = 150;
		foreach (GameObject search in hold) {
			Vector3 dif = search.transform.position - transform.position;
			float currentDist = dif.sqrMagnitude;

			if (currentDist < distance) {
				if (rHand) {
					if (rHand && lPos != search.transform.position) {
                    
						rPos = search.transform.position;

						closest = search;
						distance = currentDist;
                    
					}
				} else {
					if (!rHand && rPos != search.transform.position) {
                    
						lPos = search.transform.position;

						closest = search;
						distance = currentDist;
                    
					}

				}

			}
		}
		return closest;
	}

	//void DeadOffset()
	//{
	//    if (rightStick.x > 0)
	//    {
	//        deadzoneVector.x -= deadzone;
	//    }
	//    else
	//    {
	//        deadzoneVector.x += deadzone;
	//    }
	//    if (rightStick.y > 0)
	//    {
	//        deadzoneVector.y -= deadzone;
	//    }
	//    else
	//    {
	//        deadzoneVector.y += deadzone;
	//    }

	//}
}
