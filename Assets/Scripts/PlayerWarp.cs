using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll);
       WarpCollider warpCollider = coll.gameObject.GetComponent<WarpCollider>();
       if (warpCollider != null)
        {
            Vector3 position = transform.position;
            Collider2D collider = GetComponent<Collider2D>();
            float distance = warpCollider.getDistance();
            if (warpCollider.Vertical)
                position.x += warpCollider.isHigher() ? (collider.bounds.size.x - distance) : (distance - collider.bounds.size.x);
            else
                position.y += warpCollider.isHigher() ? (collider.bounds.size.y - distance) : (distance - collider.bounds.size.y);
            transform.position = position;
        }

    }
	
}
