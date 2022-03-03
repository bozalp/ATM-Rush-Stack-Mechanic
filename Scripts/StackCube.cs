using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackCube : MonoBehaviour
{
    Movement movement;
    private void Start()
    {
        movement = GameObject.FindObjectOfType<Movement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "collectable")
        {            
            movement.AddCube(collision.gameObject);
            collision.gameObject.tag = "Untagged";
        }
    }
}
