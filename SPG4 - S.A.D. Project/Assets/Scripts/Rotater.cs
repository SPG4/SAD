// Maximilian Törn Almö
// 10-15

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Constantly rotate object.
/// </summary>
public class Rotater : MonoBehaviour {

    public float rotationSpeed;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * ((Time.deltaTime * 10) * rotationSpeed));
    }
}
