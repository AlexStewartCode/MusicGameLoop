using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D Physics; 
    private int JumpCount; 
    [Range(0,10)] public float JumpForce;
    [Range(0,10)] public float Speed; 
    [SerializeField] List<GameObject> Characters; 
    private GameObject CurrentCharacter; 
    bool [] RightLeftCollide = {false, false}; 
    
    // Start is called before the first frame update
    void Start()
    {
        JumpCount = 2; 
        Physics = GetComponent<Rigidbody2D>();
        NewCharacterChosen(Characters[0]);
    }

    // Update is called once per frame
    void Update()
    {
        /*Get Velocity as a standalone vector
        makes it easier to edit without having to 
        constantly set the rigidbody physics 
        */
        Vector2 CurrentVelocity = Physics.velocity;
        if(Input.GetKeyDown(KeyCode.W) && JumpCount > 0)
        {
            CurrentVelocity.y = JumpForce; 
            JumpCount--;
        }

        if(Input.GetKey(KeyCode.A) && RightLeftCollide[0] == false)
        {
            CurrentVelocity.x = -Speed; 
        }

        if(Input.GetKey(KeyCode.S))
        {
            //Implement ducking 
            //1/2 height hitbox?
        }

        if(Input.GetKey(KeyCode.D) && RightLeftCollide[1] == false)
        {
            CurrentVelocity.x = Speed;
        }

        if (Input.GetKey(KeyCode.J))
        {
            JumpCount = 2;
        }

        Physics.velocity = CurrentVelocity; 

        if(Input.GetKeyDown(KeyCode.F))
        {
            CurrentCharacter.GetComponentInChildren<CharCont>().attack(); 
        }
    }
    private void NewCharacterChosen(GameObject NewCharacter)
    {
        Destroy(CurrentCharacter);
        CurrentCharacter = Instantiate (NewCharacter, this.transform);
    }

    const float CLOSE_ENGOUH = 0.001f;

    /// <summary>
    /// Checks with a margin for acceptable error defined by CLOSE_ENOUGH
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    private bool ApproxEqual(Vector2 lhs, Vector2 rhs)
    {
        bool xCloseEnough = false;
        bool yCloseEnough = false;
        if(Mathf.Abs(lhs.x - rhs.x) < CLOSE_ENGOUH)
        {
            xCloseEnough = true;
        }
        if(Mathf.Abs(lhs.y - rhs.y) < CLOSE_ENGOUH)
        {
            yCloseEnough = true;
        }

        return xCloseEnough && yCloseEnough;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject Collider = collision.gameObject; 
        Vector2 CollisionDirection = collision.contacts[0].normal.normalized;
        if(ApproxEqual(CollisionDirection, Vector2.up))
        {
            if(collision.gameObject.tag.Equals("Floor"))
            {
                JumpCount = 2; 
            }
        }
        if(ApproxEqual(CollisionDirection, Vector2.down))
        {
            
        }
        if(ApproxEqual(CollisionDirection, Vector2.left))
        {
            RightLeftCollide[1] = true; 
        }
        if(ApproxEqual(CollisionDirection, Vector2.right))
        {
            RightLeftCollide[0] = true;
        }
    }

    private void OnCollisionExit2D()
    {
        RightLeftCollide[0] = false;
        RightLeftCollide[1] = false;
    }
}