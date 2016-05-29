using UnityEngine;
using System.Collections;

public class GripColorTest : MonoBehaviour {

    float rTrigger, gripLoss;
	Renderer rend;
	Color gripFull, gripDead, currentC;

	//Notes
	// 1/255 = 0.003921568627451


	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update() {
        rend = GetComponent<Renderer>();
        rTrigger = Input.GetAxis("Right Trigger");
        //gripFull = new Color(rTrigger, 0, 0, 1);
        //gripDead = new Color(0, 1 - rTrigger, 0, 1);

        if (Input.GetAxis("Right Trigger") != 0)
        {
            rend.material.color = new Color(rTrigger, rTrigger/2, 1 - rTrigger, 1);
        }

        //if (Input.GetAxis("Right Trigger")>=0) {
        //    StartCoroutine("GripRecharge", rend.material.color, Color.blue, rTrigger, 1f);
        //}
        Debug.Log(rTrigger);


    }

	//IEnumerator GripRecharge(Color currentC, Color startColor, float gripValue){
	//	yield return null;
		
	//}
}
