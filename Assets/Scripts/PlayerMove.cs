using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D Physics; 
    private int JumpCount; 
    [Range(0,10)] public float JumpForce;
    [Range(0,10)] public float Speed; 
    // Start is called before the first frame update
    void Start()
    {
        JumpCount = 2; 
        Physics = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Get Velocity as a standalone vector
        makes it easier to edit without having to 
        */
        Vector2 CurrentVelocity = Physics.velocity;
        if(Input.GetKeyDown(KeyCode.W) && JumpCount > 0)
        {
            CurrentVelocity.y = JumpForce; 
            JumpCount--; 
        }

        if(Input.GetKey(KeyCode.A))
        {
            CurrentVelocity.x = -Speed; 
        }

        if(Input.GetKey(KeyCode.S))
        {
            
        }

        if(Input.GetKey(KeyCode.D))
        {
            CurrentVelocity.x = Speed;
        }

        if (Input.GetKey(KeyCode.J))
        {
            JumpCount = 2;
        }

        if(!Input.anyKey )
        {
            CurrentVelocity.x /= 1.01f;
        }

        Physics.velocity = CurrentVelocity; 
    }
}
