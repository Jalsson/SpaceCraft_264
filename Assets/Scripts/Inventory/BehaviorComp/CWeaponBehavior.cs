using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponBehavior : ComponentBehaviorBase {

    public GameObject projectile;
    public float lanchSpeed;
    public float FireRate;

    public override void Use()
    {
        if (currentShipComponent != null)
        {
            // Create the Bullet from the Bullet Prefab
            GameObject bullet = Instantiate(
                projectile,
                transform.position,
                transform.rotation);

            // Add velocity to the bullet
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            bulletRigidbody.velocity = ship.GetComponent<Rigidbody2D>().velocity;
            bulletRigidbody.AddForce(transform.up * lanchSpeed * 100);

            bullet.GetComponent<ProjectileBehavior>().Behavior();
        }
    }
}
