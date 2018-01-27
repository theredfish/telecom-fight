using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    public Vector2 speed = new Vector2(10, 0);
    public float impulsiontop = 1300;
    public float impulsionmiddle = 800;
    public float impulsiondown = 500;
    private float impulsion = 0;
    public Vector2 direction = new Vector2(-1, 1);
    private Rigidbody2D rigidbodyComponent;

    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        rigidbodyComponent.velocity = (new Vector2(speed.x * direction.x,0));
        Debug.Log(impulsion);
        rigidbodyComponent.AddRelativeForce(new Vector2(0, impulsion));
    }

    public void impulsionDirection(float axis)
    {
        Debug.Log("impulsiondirection"+axis);
        if(axis>0)
            impulsion = impulsiontop;
        else if (axis==0)
        {
            impulsion = impulsionmiddle;
        }
        else
        {
            impulsion = impulsiondown;
        }
    }
    void Update()
    {
        
    }

    void FixedUpdate()
    {
    }
}
