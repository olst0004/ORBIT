using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToGround : MonoBehaviour
{
    public Transform _Planet;
    public Transform _Orbit;
    public LineRenderer _lineRenderer;
    public GroundQuad _groundQuad;
	
	void Update ()
    {
        UpdateLinePositions();
    }
    
    public void UpdateLinePositions()
    {
        RaycastHit hit;
        Vector3 rayDown = Vector3.zero;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, transform.position.y))
        {
            rayDown = hit.point;
        }
        else
        {
            rayDown = transform.position - Vector3.up * transform.position.y;
        }

        _lineRenderer.SetPosition(0, -_Planet.up * _Planet.localScale.y * 0.5f);    //Line ends with Planet
        //_lineRenderer.SetPosition(0, -_Planet.up * _Orbit.localScale.x * 0.5f);   //Line ends with Orbit
        _lineRenderer.SetPosition(1, rayDown - transform.position);

        _groundQuad.UpdatePosition(rayDown - transform.position);
    }
}
