using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CThrusterBehavior : ComponentBehaviorBase {

    public float thrusterPower;
    Rigidbody2D rigi2D;

    private void OnEnable()
    {
        rigi2D = PlayerStats.instance.GetComponent<Rigidbody2D>();
    }

    public override void Use()
    {
        Invoke("Thrust", 0.2f);

    }

    private void Thrust()
    {
        thrusterPower = PlayerStats.instance.thrusterPower * 10;
        rigi2D.AddForce(transform.up * thrusterPower);
    }

}
