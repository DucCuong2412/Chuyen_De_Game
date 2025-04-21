using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawn : NetworkBehaviour
{
    public static ScoreSpawn sharedInstance;

    public List<GameObject> pooledObjects = new List<GameObject>();
    public NetworkPrefabRef prefab;
    public int amountToPool = 20;


    private void Awake()
    {
        sharedInstance = this;
    }

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            InitPool();
        }
    }

    private void InitPool()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-5, 10), 1, Random.Range(-10, 10));


            NetworkObject obj = Runner.Spawn(prefab, spawnPos, Quaternion.identity, null);
            obj.gameObject.SetActive(false); // Ẩn đối tượng ban đầu
                                             // pooledObjects.Add(obj.gameObject);
            Rpc_HideObject(obj);
        }

        Debug.Log($"[ScoreSpawn] Spawned {amountToPool} objects to pool (host only).");
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true); // Kích hoạt đối tượng khi lấy ra từ pool
                return obj;
            }
        }

        return null;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_HideObject(NetworkObject netObj)
    {
        netObj.gameObject.SetActive(true);
    }

}
