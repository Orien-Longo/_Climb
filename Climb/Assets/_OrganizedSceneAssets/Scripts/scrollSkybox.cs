﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollSkybox : MonoBehaviour {

    public Skybox airCube;
    float airCubeRot = 0;
    float rotationSpeed = 10f;

	// Use this for initialization
	void Start () {
        airCube = GetComponent<Skybox>();
        //airCube = GetComponent<Skybox>().GetComponent<;
	}
	
	// Update is called once per frame
	void Update () {
        airCubeRot = airCube.material.GetFloat("Rotation");
        if (airCubeRot < 360f)
        {
            airCubeRot += rotationSpeed * 10f;
        }
        else
        {
            airCubeRot = 0f;
        }
        Debug.Log("airCubeRot");
    }
}
