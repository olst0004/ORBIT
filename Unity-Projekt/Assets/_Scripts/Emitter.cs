using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Emitter : MonoBehaviour {
    
    #region 
    [Header("Emission")]
    public GameObject _MatterPrefab;
	public float Lifespan = 10;
    public float MaxSpeed = 10;
    [Range(0.01f, 0.1f)]
    public float CountdownToSpawn = 1;
    [Range(0, 1f)]
    public float ScatterRadius = 0.5f;
    #endregion

    #region 
    [Header("Particle")]
    public Color _MatterColor;
    [Range(1, 10)]
    public float _MatterSpeed = 1;
    public GameObject _MatterParent;
    #endregion

    List<GameObject> _Orbit;

    float Counter;
	int Objectcounter = 0;
    
	void Start ()
    {
        Counter = CountdownToSpawn;
	}
    
	void Update ()
    {
		Counter+=Time.deltaTime;
		if (Counter >= CountdownToSpawn)
		{
			Spawner(Objectcounter);
			Counter = 0;
		}
	}
    
	void Spawner(int in_objectCounter)
    {
        Vector3 rndm = Vector3.up * Random.Range(-1f, 1f) * ScatterRadius + Vector3.right * Random.Range(-1f, 1f) * ScatterRadius;

		GameObject NewSpawn = (GameObject)Instantiate(_MatterPrefab, transform.position + rndm, Quaternion.identity);

		NewSpawn.name = _MatterParent.GetInstanceID().ToString() + "_Matter "+ in_objectCounter;
        NewSpawn.transform.parent = _MatterParent.transform;

        Matter matter = NewSpawn.GetComponent<Matter>();
        matter.SpawnMatter(transform.forward * _MatterSpeed, _MatterColor, Lifespan, MaxSpeed);
 
        if (_Orbit != null)
        {
            for (int i = 0; i < _Orbit.Count; i++)
            {
                matter.myOrbit.Add(false);
                matter.orbitVector.Add(new Vector3(0, 0, 0));
            }
        }

		Objectcounter++;
	}

    //public void SetEmitter(GameObject MatterParent, List<GameObject> Orbit)
    //{
    //    _MatterParent = MatterParent;
    //    _Orbit = Orbit;
    //}
}
