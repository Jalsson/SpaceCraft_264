using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroyer : MonoBehaviour {

    public GameObject[] smallerAsteroids ;
    public GameObject parentObject;
    float minForce = 600;
    float maxForce = 750;
    private void Start()
    {
        parentObject = GetComponentInParent<PolygonCollider2D>().gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float colliderSpeed = collision.relativeVelocity.magnitude;
        if(colliderSpeed >= 7)
        {
            DestroyToSmallPieces();
            
        }
            
    }

    public void DestroyToSmallPieces()
    {
        parentObject.GetComponent<PolygonCollider2D>().enabled = false;
        parentObject.GetComponent<SpriteRenderer>().enabled = false;
        for (int i = 0; i < Random.Range(5f, 7f); i++)
        {
            int direction = Random.Range(0, 1);
            GameObject clone;
            clone = Instantiate(smallerAsteroids[Random.Range(0, smallerAsteroids.Length)], transform.position, Quaternion.identity) as GameObject;
            clone.GetComponent<PreventClipping>().spawnedRecent = false;
            switch (direction)
            {
                case 0:
                    clone.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(minForce, maxForce);
                    break;
                case 1:
                    clone.GetComponent<Rigidbody2D>().angularVelocity = -Random.Range(minForce, maxForce);
                    break;

            }
            Invoke ("DestroyParent",1f);
          

        }
    }

    private void DestroyParent()
    {
        Destroy(parentObject);
    }
}
