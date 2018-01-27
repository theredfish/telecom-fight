using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    public Vector2 speed = new Vector2(10, 0);
    public float impulsion = 1000;
    public Vector2 direction = new Vector2(-1, 1);
    private Rigidbody2D rigidbodyComponent;

    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        rigidbodyComponent.velocity = (new Vector2(speed.x * direction.x,0));
        rigidbodyComponent.AddRelativeForce(new Vector2(0, impulsion));
    }
    void Update()
    {
        
    }

    void FixedUpdate()
    {
    }
}
