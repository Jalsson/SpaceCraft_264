using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {


    public int itemScore;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
     PlayerStats playerStats = collision.transform.GetComponent<PlayerStats>();

        playerStats.ChangeScore(itemScore);
            collision.transform.position = RandomItemSpawner.instance.GetRandomSpawnPositionInside();
            //ääni efekti
        }
    }


}
