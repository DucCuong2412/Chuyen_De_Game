using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawn : NetworkBehaviour
{
    [Header("Prefab cần spawn (gán từ inspector)")]
    public NetworkPrefabRef scorePrefab;

    [Header("Vị trí spawn (có thể gán 3 điểm trong scene)")]
    public Transform[] spawnPoints;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            SpawnScores();
        }
    }

    private void SpawnScores()
    {
        int count = Mathf.Min(3, spawnPoints.Length); // chỉ spawn 3

        for (int i = 0; i < count; i++)
        {
            Runner.Spawn(scorePrefab, spawnPoints[i].position, Quaternion.identity, null);
        }

        Debug.Log("Đã spawn 3 item score trên map.");
    }
}

