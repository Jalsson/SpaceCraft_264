using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{

    #region singelton
    public static RandomItemSpawner instance;
    void Awake()
    {
        instance = this;
    }
#endregion

    [System.Serializable]
    public class Pool
    {
        public GameObject[] prefab;
        public string tag;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public float minimumNum;
    public float maximumNum;
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab[Random.Range(0,pool.prefab.Length)]);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);

            for (int i = 0; i < pool.size; i++)
            {
                SpawnFromPool(pool.tag);
            }
        }
    }

    public GameObject SpawnFromPool(string tag)
    {

        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag " + tag + " dosen't excist.");
            return null;
        }

        GameObject objectSpawned = poolDictionary[tag].Dequeue();
        objectSpawned.SetActive(true);
        objectSpawned.transform.position = GetRandomSpawnPositionInside();

        PreventClipping preventclipping = objectSpawned.GetComponent<PreventClipping>();
        preventclipping.spawnedRecent = true;
        StartCoroutine(preventclipping.MoveIfColliding());
        if (objectSpawned.GetComponent<Rigidbody2D>() != null)
        {
            objectSpawned.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-Random.Range(40f, 70f), Random.Range(40f, 70f));
        }

        poolDictionary[tag].Enqueue(objectSpawned);
        return objectSpawned;
    }

    public Vector2 GetRandomSpawnPositionInside()
    {

        Vector2 rndPosWithin;
        int side = Random.Range(0, 4);
        float verticalPoint;
        float horizontalPoint;

        switch (side)
        {
            case 0:
                verticalPoint = Random.Range(-minimumNum, -maximumNum);
                horizontalPoint = Random.Range(-maximumNum, maximumNum);
                break;
            case 1:
                verticalPoint = Random.Range(minimumNum, maximumNum);
                horizontalPoint = Random.Range(-maximumNum, maximumNum);
                break;
            case 2:
                verticalPoint = Random.Range(-maximumNum, maximumNum);
                horizontalPoint = Random.Range(-minimumNum + 10, -maximumNum);
                break;
            case 3:
                verticalPoint = Random.Range(-maximumNum, maximumNum);
                horizontalPoint = Random.Range(minimumNum - 10, maximumNum);
                break;
            default:
                verticalPoint = Random.Range(-maximumNum, maximumNum);
                horizontalPoint = Random.Range(minimumNum - 10, maximumNum);
                break;
        }

        rndPosWithin = new Vector2(horizontalPoint + transform.position.x, verticalPoint + transform.position.y);
        return rndPosWithin;


    }

}
