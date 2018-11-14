using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class TargetLight : MonoBehaviour 
{
    [HideInInspector]
	public Vector3 _TargetPos;

	Light light;

	public void LookAtTarget()
	{
		float distance = (_TargetPos - transform.position).magnitude;
		transform.rotation = Quaternion.LookRotation (_TargetPos - transform.position);
		_TargetPos = transform.forward * distance + transform.position;
	}

	public void SetTargetPos(Vector3 in_newPos)
	{
		_TargetPos = in_newPos;
		LookAtTarget ();
	}

	public Color GetLightColor()
	{
		if (light == null)
			light = GetComponent<Light> ();

		return light.color;
	}

	public float GetLightIntensity()
	{
		if (light == null)
			light = GetComponent<Light> ();

		return light.intensity;
	}
}
