using UnityEngine;
using System.Collections;

public class ArmRay : MonoBehaviour {

    public GameObject rayStart;
    public GameObject rayEnd;

    public GameObject rayBaby;

    public Vector3 rsV;
    public Vector3 reV;

	void Start () {

        Instantiate<GameObject>(rayBaby);
        rayBaby.transform.parent = rayStart.transform;
        rayBaby.transform.position = rayStart.transform.position;
        rayBaby.transform.localRotation = Quaternion.Euler(0,0,0);
        
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = rayStart.transform.position;
        rsV = rayBaby.transform.localPosition;
        reV = rayEnd.transform.position;
        RaycastHit hit;
        Debug.DrawRay(rsV, reV, Color.green);

	}
}
