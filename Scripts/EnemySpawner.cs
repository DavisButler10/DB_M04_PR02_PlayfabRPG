using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviourPun
{
    public string enemyPrefabPath;
    public float maxEnemies;
    public float spawnRadius;
    public float spawnCheckTime;
    public bool isNearPlayer = false;

    private float lastSpawnCheckTime;
    private List<GameObject> curEnemies = new List<GameObject>();

    private void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = spawnRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (Time.time - lastSpawnCheckTime > spawnCheckTime)
        {
            lastSpawnCheckTime = Time.time;
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        // remove any dead enemies from the curEnemies list
        for (int x = 0; x < curEnemies.Count; ++x)
        {
            if (!curEnemies[x])
                curEnemies.RemoveAt(x);
        }

        // if we have maxed out our enemies, return
        if (curEnemies.Count >= maxEnemies)
            return;

        // otherwise, spawn an enemy
        if (isNearPlayer)
        {
            Vector3 randomInCircle = Random.insideUnitCircle * spawnRadius;
            GameObject enemy = PhotonNetwork.Instantiate(enemyPrefabPath, transform.position + randomInCircle, Quaternion.identity);
            curEnemies.Add(enemy);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isNearPlayer = true;
        }
        else
        {
            isNearPlayer = false;
        }
    }
}



