using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePrefab : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!Object.IsValid) return;


        if (other.gameObject.CompareTag("Player"))
        {
            if (Object.HasStateAuthority)
            {
                gameObject.SetActive(false); // Ẩn đối tượng khi người chơi ăn nó
            }
            else
            {
                RPC_RequestHide();
            }
        }
    }


    [Rpc(RpcSources.All, RpcTargets.All)] // Nếu không, gọi host ra để cùng ẩn đi
    void RPC_RequestHide()
    {
        gameObject.SetActive(false);
    }


}
