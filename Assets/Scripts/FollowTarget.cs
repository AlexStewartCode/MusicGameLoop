using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] GameObject Target; 
    [SerializeField] float SmoothingSpeed; 
    Vector3 EndPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        EndPoint = transform.position;
    }

    // 
    void Update()
    {
        EndPoint = Target.transform.position;
        EndPoint.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, EndPoint, 
            SmoothingSpeed * Time.deltaTime);
    }
}
