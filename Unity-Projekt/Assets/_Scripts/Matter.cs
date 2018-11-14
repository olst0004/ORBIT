using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Matter : MonoBehaviour
{
    #region 
    [Header("Components")]
    public Transform _Transform;
    public Rigidbody _RigidBody;
    public LineRenderer _LineRenderer;
    public AudioSource _AudioSource;
    #endregion

    #region 
    [Header("Orbit")]
    public Vector3 myVector;
    public List<Vector3> orbitVector;
    public List<bool> myOrbit;
    #endregion

    #region
    [Header("Speed")]
    public float Lifespan;
    public float MaxSpeed;
    public Color _Color;
    public int LineResolution = 10;
    Vector3[] PosArray;
    #endregion

    #region 
    [Header("Sound")]
    public AudioClip EnterOrbit;
    public AudioClip LeaveOrbit;
    public AudioClip ArriveAtDestination;
    public AudioClip Crash;
    float lowPitch = 0.9f;
    float highPitch = 1.1f;
    #endregion

    public void SpawnMatter(Vector3 in_MatterVector, Color in_MatterColor, float in_LifeSpan, float in_maxSpeed)
    {
        myVector = in_MatterVector;
        _Color = in_MatterColor;
        Lifespan = in_LifeSpan;
        MaxSpeed = in_maxSpeed;

        _LineRenderer.positionCount = LineResolution;
        PosArray = new Vector3[LineResolution];
        
        for (int i = 0; i < PosArray.Length; i++)
        {
            _LineRenderer.SetPosition(i, transform.position);
        }
        
        _LineRenderer.startColor = _LineRenderer.endColor = _Color;
    }

    void FixedUpdate ()
    {
        AdjustLifeSpan();
        AdjustVelocity();
    }

    void LateUpdate()
    {
        AdjustLine();
    }

    void AdjustLifeSpan()
    {
        Lifespan -= Time.fixedDeltaTime;
        if (Lifespan < 0)
        {
            StopMatter();
        }
    }

    void AdjustVelocity()
    {
        _RigidBody.velocity = AddVec(myVector, orbitVector);
        if (myVector.magnitude > MaxSpeed)
        {
            StopMatter();
        }
        else
        {
            myVector = _RigidBody.velocity;
        }
    }

    void AdjustLine()
    {
        for (int i = 0; i < LineResolution - 1; i++)
        {
            PosArray[i] = PosArray[i + 1];
            if (PosArray[i].sqrMagnitude != 0)
                _LineRenderer.SetPosition(i, PosArray[i]);
        }

        PosArray[LineResolution - 1] = transform.localPosition;
        _LineRenderer.SetPosition(LineResolution - 1, transform.localPosition);
    }

   //public void TeleportMe(Vector3 OtherTeleporterPosition, Teleporter OtherTeleporter, Vector3 newMatterVector)
   // {
   //     //Neue Matter am andern Teleporter spawnen
   //     GameObject Duplicate = (GameObject)Instantiate(gameObject, OtherTeleporterPosition, Quaternion.identity);
   //     Duplicate.name = "d_" + name;
   //     OtherTeleporter.AddLockedMatter(Duplicate);
   //
   //     Matter DuplicteMatter = Duplicate.GetComponent<Matter>();
   //     DuplicteMatter.myVector = newMatterVector;
   //     //Quaternion rot = Quaternion.Euler(RotationVector);
   //     //DuplicteMatter.myVector = rot * OtherTeleporter.gameObject.transform.up;
   //     //DuplicteMatter.myVector = Quaternion.AngleAxis(CollisionAngle, OtherTeleporter.gameObject.transform.forward) * TeleportVector;
   //
   //     //Alte Matter stoppen
   //     StopMatter();
   // }

    public void StopMatter()
    {
        myVector = Vector3.zero;
        Destroy(GetComponent<SphereCollider>());
        for (int i = 0; i < myOrbit.Count; i++)
        {
            myOrbit[i] = false;
            orbitVector[i] = Vector3.zero;
        }
        StartCoroutine(KillCoroutine());
    }

    public void ChangeColor(Color newColor)
    {
        GameObject Duplicate = (GameObject)Instantiate(gameObject, transform.localPosition + myVector * 0.01f, Quaternion.identity);
        Duplicate.name = "c_" + name;
        Duplicate.transform.parent = transform.parent;

        Matter DuplicteMatter = Duplicate.GetComponent<Matter>();
        DuplicteMatter._Color = newColor;
        
        StopMatter();
    }

    IEnumerator KillCoroutine()
    {
        for (;;)
        {
            if (PosArray[0] == PosArray[PosArray.Length - 1])
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    Vector3 AddVec(Vector3 a, Vector3 b)
    {
        Vector3 returner = new Vector3();
        returner.x = a.x + b.x;
        returner.y = a.y + b.y;
        returner.z = a.z + b.z;
        return returner;
    }

    Vector3 AddVec(Vector3 a, List<Vector3> b)
    {
        Vector3 returner = a;
        foreach (Vector3 item in b)
        {
            returner.x += item.x;
            returner.y += item.y;
            returner.z += item.z;
        }
        return returner;
    }

    public void PlayAudio(AudioClip Clip)
    {
        _AudioSource.pitch = Random.Range(lowPitch, highPitch);
        _AudioSource.PlayOneShot(Clip);
    }
}
