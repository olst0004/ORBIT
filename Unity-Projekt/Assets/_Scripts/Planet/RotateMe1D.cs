using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMe1D : MonoBehaviour
{
    [Range(0, 10)]
    public float RotationSpeed = 1f; 
    Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }
    
    void Update ()
    {
        _transform.localEulerAngles += Vector3.forward * RotationSpeed;
	}
}
