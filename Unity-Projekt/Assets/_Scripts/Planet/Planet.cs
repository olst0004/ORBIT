using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public LineToGround _LineToGround;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateLineToGround()
    {
        _LineToGround.UpdateLinePositions();
    }
}
