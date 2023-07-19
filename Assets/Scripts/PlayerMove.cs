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
    ContactPoint2D [] tempCollisionIndicators;
    
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

        if(Input.GetKey(KeyCode.A))
        {
            CurrentVelocity.x = -Speed; 
        }

        if(Input.GetKey(KeyCode.S))
        {
            //Implement ducking 
            //1/2 height hitbox? 
        }

        if(Input.GetKey(KeyCode.D))
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Floor"))
        {
            tempCollisionIndicators = collision.contacts;
            Debug.Log("HERE: " + collision.contacts.Length);
            //reset jumpcount to max value 
            JumpCount = 2; 
        }
    }

    private void OnDrawGizmos()
    {
        if(tempCollisionIndicators != null && tempCollisionIndicators.Length > 0)
        {
            Vector2 mainpoint = tempCollisionIndicators[0].point; 
            for(int i = 1; i < tempCollisionIndicators.Length; i++)
            {
                mainpoint += tempCollisionIndicators[i].point;
            }
            mainpoint /= tempCollisionIndicators.Length;
            Gizmos.DrawWireSphere(mainpoint, 1);
        }
    }
}