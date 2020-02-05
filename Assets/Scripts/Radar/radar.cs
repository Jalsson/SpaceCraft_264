using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class radar : MonoBehaviour {

    public string[] tagsAllowedToTrack;
    public List<GameObject> itemsInRange;
    public List<Vector3> itemPostions;
    public GameObject radarMark;
    public Image[] tagImages;
    public float markerUpdateRate;

    public Transform helpTransform;
    public float swichDistance;
    private void Start()
    {
        StartCoroutine(UpdateMarkerPositions());
    }
#region Collsions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < tagsAllowedToTrack.Length; i++)
        {
            if (collision.tag == tagsAllowedToTrack[i])
            {
                if (GetComponentInParent<radar>() != null)
                {
                    itemPostions.Add(collision.transform.position);
                    itemsInRange.Add(collision.gameObject);
                }    
            }
        }
        if (collision.tag == tagsAllowedToTrack[2] && collision.isTrigger == true)
            AddTextToLog.instance.AddTextToEventLog("Radar has detected " + tagsAllowedToTrack[2]);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveItemFromRadarList(collision);
    }
#endregion
    IEnumerator RemoveItemFromRadarList(Collider2D collision)
    {
        yield return new WaitForSeconds(0.1f);
        if (collision.gameObject != null)
        {


            for (int i = 0; i < tagsAllowedToTrack.Length; i++)
            {
                if (collision.tag == tagsAllowedToTrack[i])
                {
                    var listRange = itemsInRange.IndexOf(collision.gameObject);
                    itemPostions.RemoveAt(listRange);

                    itemsInRange.Remove(collision.gameObject);
                    if (collision.GetComponentInChildren<RadarObject>() != null)
                        Destroy(collision.GetComponentInChildren<RadarObject>().gameObject);
                }
            }
        }
    }
#region Update Colors and Position from input from Hud
    public void UpdateColors()
    {
        for (int i = 0; i < itemsInRange.Count; i++)
        {
            for (int t = 0; t < tagsAllowedToTrack.Length; t++)
            {
                if (itemsInRange[i].tag == tagsAllowedToTrack[t])
                {
                    var radarObject = itemsInRange[i].GetComponentInChildren<RadarObject>();
                    radarObject.GetComponent<SpriteRenderer>().color = tagImages[t].color;
                }
            }
        }
    }

    public void MarkerUpdateRate(string updateRate)
    {
        markerUpdateRate = int.Parse(updateRate);
    }
#endregion

    private void Update() //this stuff handles object positions and makes sure they dont go over the radar background
    {

        for (int i = 0; i < itemsInRange.Count; i++)
        {

            if (itemsInRange[i] == null)
            {
                itemPostions.RemoveAt(i);
                itemsInRange.RemoveAt(i);
            }
            if (itemsInRange[i] != null)                                           
            {
                if (itemsInRange[i].GetComponentInChildren<RadarObject>() != null)
                {
                    if (Vector2.Distance(new Vector2(itemPostions[i].x, itemPostions[i].y), new Vector2(transform.position.x, transform.position.y)) > swichDistance)
                    {
                        helpTransform.LookAt(itemPostions[i]);
                        itemsInRange[i].GetComponentInChildren<RadarObject>().gameObject.transform.position = transform.position + swichDistance * helpTransform.forward;

                    }
                    else
                        itemsInRange[i].GetComponentInChildren<RadarObject>().gameObject.transform.position = itemPostions[i];
                }
            }
        }
    }

    public void CreateRadarObjects(Sprite objectShape, int placeInList,Color spriteColor)
    {
        var createdObject = Instantiate(radarMark, itemPostions[placeInList], Quaternion.identity);
        createdObject.GetComponent<SpriteRenderer>().sprite = objectShape;
        createdObject.transform.SetParent(itemsInRange[placeInList].transform);
        createdObject.GetComponent<SpriteRenderer>().color = spriteColor; 
    }



    public IEnumerator UpdateMarkerPositions()
    {
        for (int i = 0; i < itemsInRange.Count; i++)
        {
            if (itemsInRange[i] != null)
            {
                if (itemsInRange[i].GetComponentInChildren<RadarObject>() == null)
                {
                    Color markerColor = new Color(255, 255, 255) ;
                    for (int t = 0; t < tagsAllowedToTrack.Length; t++)
                    {
                        if (itemsInRange[i].tag == tagsAllowedToTrack[t])
                        {
                            markerColor = tagImages[t].color;
                        }
                    }
                    CreateRadarObjects(itemsInRange[i].GetComponent<SpriteRenderer>().sprite, i, markerColor);
                }
                else
                {
                    itemPostions[i] = itemsInRange[i].transform.position;
                }
            }
        }
        yield return new WaitForSecondsRealtime(markerUpdateRate);
        StartCoroutine(UpdateMarkerPositions());
    }


}
