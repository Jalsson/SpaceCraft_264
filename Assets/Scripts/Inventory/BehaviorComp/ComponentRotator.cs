using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentRotator : MonoBehaviour {

   public float smoothing = 0.1f;

    // Use this for initialization
    void Start () {
        StartCoroutine(Rotator());
	}
	

    IEnumerator Rotator()
    {
        transform.Rotate(Vector3.forward * 3);

        yield return new WaitForSeconds(smoothing);
        StartCoroutine(Rotator());
    }
}
