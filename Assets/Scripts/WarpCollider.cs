using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpCollider : MonoBehaviour {
    public Transform linkedWarp;
    public bool Vertical;
    private float warpoffset;
    private bool higher;

    void Start()
    {   
        Vector3 linkedpos = linkedWarp.position;
        Vector3 position = transform.position;
        if (Vertical)
        {
            higher = position.x > linkedpos.x;
            warpoffset= Mathf.Abs(linkedpos.x)+Mathf.Abs(position.x);
        }
        else
        {
            higher = position.y > linkedpos.y;
            warpoffset = Mathf.Abs(linkedpos.y)+Mathf.Abs(position.y);
        }
    }


    public bool isHigher()
    {
        return higher;
    }
    public float getDistance()
    {
        return warpoffset;
    }

}
