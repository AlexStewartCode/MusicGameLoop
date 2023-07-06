using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] GameObject Target; 
    [SerializeField] float SampleRate; 
    Queue<Vector3> Path; 
    Vector3 StartPoint; 
    Vector3 EndPoint;
    double StartTime;
    float deltaTime; 

    public AnimationCurve curve; 
    // Start is called before the first frame update
    void Start()
    {
        Path = new Queue<Vector3>();
        StartTime = Time.timeAsDouble;
        StartPoint = transform.position;
        EndPoint = transform.position;
        deltaTime = 0;
    }

    // 
    void Update()
    {
        deltaTime += Time.deltaTime;
        if(Time.timeAsDouble - StartTime > SampleRate)
        {
            Vector3 temp = Target.transform.position; 
            temp.z = transform.position.z;
            Path.Enqueue(temp);
            
            StartPoint = EndPoint; 
            EndPoint = Path.Dequeue();
            StartTime = Time.timeAsDouble;
            deltaTime = 0; 
        }
        
        transform.position = Vector3.Lerp(StartPoint, EndPoint, deltaTime/SampleRate);
    }
}
