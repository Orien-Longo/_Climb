using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

    GameObject ends;
    float dist;
    GameObject[] hold;
    bool snapped;
    public float snapDist;

    public bool rHand;
    static public Vector3 rPos;
    static public Vector3 lPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Snap();
    }

    void Snap()
    {
        ends = ClosestHold();

        if (ends != null)
        {
            dist = Vector3.Distance(ends.transform.position, transform.position);

            if (Mathf.Abs(dist) < snapDist)
            {
                snapped = true;
                transform.position = ends.transform.position;
                transform.rotation = ends.transform.rotation;
            }
            else {
                if (rHand)
                {
                    rPos = Vector3.zero;
                }
                else {
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
                if (rHand && lPos != search.transform.position)
                {
                    rPos = search.transform.position;

                    closest = search;
                    distance = currentDist;
                }
                if (!rHand && rPos != search.transform.position)
                {
                    lPos = search.transform.position;

                    closest = search;
                    distance = currentDist;
                }
            }
        }
        return closest;
    }
}
