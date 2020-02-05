using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour {

    public float velocity;
    public float lastRelativeVelocityToObject;

    public void FixedUpdate()
    {
        velocity = GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lastRelativeVelocityToObject = collision.relativeVelocity.magnitude;
    }
}
