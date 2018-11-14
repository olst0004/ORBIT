using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundQuad : MonoBehaviour
{
    [Range(0, 0.1f)]
    public float Offset;

	public void UpdatePosition (Vector3 in_Pos)
    {
        transform.localPosition = in_Pos + Vector3.up * Offset;
        transform.LookAt(transform.position - Vector3.up);
	}
}
