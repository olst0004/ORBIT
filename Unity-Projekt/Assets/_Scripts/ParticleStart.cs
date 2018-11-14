using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStart : MonoBehaviour {

    ParticleSystem _ParticleSystem;
    [Range(0, 5)]
    public float TimeTillRampedUp = 1;

    
	void Start ()
    {
        _ParticleSystem = GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule PtShape = _ParticleSystem.shape;
        PtShape = _ParticleSystem.shape;
        PtShape.arc = 0;
	}
	
	void Update ()
    {
        ParticleSystem.ShapeModule PtShape = _ParticleSystem.shape;
        if (Time.realtimeSinceStartup / TimeTillRampedUp < 1)
        {
        PtShape.arc = Mathf.Lerp(0, 360, Time.realtimeSinceStartup / TimeTillRampedUp);
        }
        else
        {
            PtShape.arc = 360;
            Destroy(this);
        }
	}
}
