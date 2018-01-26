using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {
    public Transform[] spawnPoints;
    int randomSpawnPoint;

    // Update is called once per frame
    void SpawnAPlayer(GameObject player)
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        player.transform.position = spawnPoints[randomSpawnPoint].position;
    }
}

