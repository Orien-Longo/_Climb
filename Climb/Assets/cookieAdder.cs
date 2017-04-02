using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookieAdder : MonoBehaviour {
    //public Texture SunCookie;
    float cookie, cookieRate;

	// Use this for initialization
	void Start () {

        gameObject.GetComponent<Light>().cookieSize = 10;
        cookieRate = .05f;
	}

    // Update is called once per frame
    void Update() {
        if (cookie < 70f)
        {
            gameObject.GetComponent<Light>().cookieSize += cookieRate;

            //Debug.Log(gameObject.GetComponent<Light>().cookieSize);
        }
        if (cookie > 70f)
        {
            gameObject.GetComponent<Light>().cookieSize = 0;
        }
	}
}
