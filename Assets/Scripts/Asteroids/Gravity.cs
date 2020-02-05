using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public float pullRadius;
    public float pullForce;
    public CircleCollider2D circleCollider2D;

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = pullRadius;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            if (collision.attachedRigidbody.mass <= 0.8)
            {
                if (Vector2.Distance(collision.transform.position, circleCollider2D.transform.position) <= 3)
                {
                    //kun tulee liian lähelle niin työntää pois

                    Vector2 forceDirection = transform.position - collision.transform.position;
                    collision.GetComponent<Rigidbody2D>().AddForce(forceDirection.normalized * -pullForce * Time.fixedDeltaTime / 2);

                }
                else
                {
                    //muuten vetää luokseen
                    Vector2 forceDirection = transform.position - collision.transform.position;
                    collision.GetComponent<Rigidbody2D>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime / 10);
                }
            }

             if (collision.attachedRigidbody.mass >= 2)
            {
                Vector2 forceDirection = transform.position - collision.transform.position;
                collision.GetComponent<Rigidbody2D>().AddForce(forceDirection.normalized * -pullForce * Time.fixedDeltaTime / 2);
            }
        }
    }
}
