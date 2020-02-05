using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public bool alsoRotation = false;
    public GameObject player;      
    private Vector3 offset;       

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        if (alsoRotation)
            transform.rotation = player.transform.rotation;
    }

}
