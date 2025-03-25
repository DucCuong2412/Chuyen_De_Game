using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waepon : NetworkBehaviour
{
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcSayHello(string name)
    {
        Debug.Log($"hello from another player: {name}");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var id = Runner.LocalPlayer.PlayerId;
            RpcSayHello($"player {id}");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var targetPlayer=other.gameObject.GetComponent<NetworkObject>().InputAuthority;
            RpcApplyDameToPlayer(targetPlayer, 10);
        }
    }

    public void RpcApplyDameToPlayer(PlayerRef targetPlayer,int damege)
    {
        Runner.TryGetPlayerObject(targetPlayer, out var plObject);
        if(plObject == null)
        {
            return;

        }
        plObject.GetComponent<PlayerProperties>().TakeDamege(damege);
    }

}
