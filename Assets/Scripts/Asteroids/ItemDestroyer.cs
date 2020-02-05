using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour {

    public string[] respawnItemTags;

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < respawnItemTags.Length; i++)
        {
            if (collision.tag == respawnItemTags[i])
            {
                SpawnAgain(collision);
            }
        }
    }

    private void SpawnAgain(Collider2D collision)
    {
        collision.transform.position = RandomItemSpawner.instance.GetRandomSpawnPositionInside();
        var preventclipping = collision.GetComponent<PreventClipping>();
        preventclipping.spawnedRecent = true;
        StartCoroutine(preventclipping.MoveIfColliding());
    }
}
