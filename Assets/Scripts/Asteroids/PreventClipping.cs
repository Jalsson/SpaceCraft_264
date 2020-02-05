using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PreventClipping : MonoBehaviour {

    public bool spawnedRecent = false;
    RandomItemSpawner randomItemSpawner;
    GameObject player;

    private void Awake()
    {
        player = PlayerStats.instance.gameObject;
        randomItemSpawner = RandomItemSpawner.instance;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spawnedRecent && collision.tag == "BackGroundImage" && collision.tag != "Player")
        {
            collision.transform.position = randomItemSpawner.GetRandomSpawnPositionInside();
            spawnedRecent = true;
            StartCoroutine(MoveIfColliding());
        }

    }
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject != player)
        {
            if (spawnedRecent)
            {
                collider.transform.position = randomItemSpawner.GetRandomSpawnPositionInside();
                spawnedRecent = true;
                StartCoroutine(MoveIfColliding());
            }
        }
    }
    public IEnumerator MoveIfColliding()
    {

        yield return new WaitForSeconds(0.2f);
        spawnedRecent = false;
    }
}
