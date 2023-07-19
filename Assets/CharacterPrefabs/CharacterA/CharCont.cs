using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCont : MonoBehaviour
{
    GameObject Weapon; 
    //Multiple hitboxes for weapon effects etc
    [SerializeField] public List <BoxCollider2D> Hitboxes; 

    // Start is called before the first frame update
    void Start()
    {
        Weapon = gameObject.transform.GetChild(1).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attack()
    {
        //play attack animation
        Weapon.transform.Rotate(Vector3.back, 60);
        //hitboxes for weapon 

    }
}
