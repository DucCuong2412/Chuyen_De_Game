using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAcive : NetworkBehaviour
{
    // Start is called before the first frame update
    private float timer = 0f;
    float spawnInterval = 2f; // Thay đổi khoảng thời gian giữa các lần sinh đối tượng





    public override void FixedUpdateNetwork()
    {
       if(!Object.HasInputAuthority)
            return;

        // Di chuyển khi GameStarted = true
        timer += Runner.DeltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            Rpc_showUp();// Gọi hàm RPC để đồng bộ việc hiện item
        }

    }



    [Rpc(RpcSources.StateAuthority, RpcTargets.All)] // Nếu không, gọi host ra để cùng ẩn đi
    void Rpc_showUp()
    {
        GameObject score = ScoreSpawn.sharedInstance.GetPooledObject();

        if (score != null)
        {
            score.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

            score.SetActive(true);

        }
        else
        {
            Debug.Log("No available objects in the pool.");
        }

    }


}
