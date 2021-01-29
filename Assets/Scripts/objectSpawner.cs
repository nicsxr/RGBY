using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] spawnObjs;
    private float spawnTime;

    
    void Start()
    {
        spawnTime = Random.Range(5, 20);
    }

    void Update()
    {
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)
        {
            Instantiate(spawnObjs[Random.Range(0, spawnObjs.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)]);
            spawnTime = Random.Range(5, 20);
        }
    }

}
