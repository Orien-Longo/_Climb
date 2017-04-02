using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollSkybox : MonoBehaviour {

    public Skybox airCube;
    float airCubeRot = 0;
    float rotationSpeed = .2f;

	// Use this for initialization
	void Start () {
        airCube = GetComponent<Skybox>();
        //airCube = GetComponent<Skybox>().GetComponent<;
	}
	
	// Update is called once per frame
	void Update () {
        airCubeRot = airCube.material.GetFloat("Rotation");
        airCubeRot += rotationSpeed;
        Debug.Log("airCubeRot");
    }
}
