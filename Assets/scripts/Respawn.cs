using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : NetworkBehaviour
{
    public GameObject playerPrefab;

    public void RespawnPlayer(PlayerRef playerRef)
    {
        StartCoroutine(RespawnCoroutine(playerRef));
    }

    private IEnumerator RespawnCoroutine(PlayerRef playerRef)
    {
        yield return new WaitForSeconds(5f);

        if (Runner.IsRunning)
        {
            Runner.Spawn(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity, playerRef, (runner, obj) =>
            {
                var playerSetup = obj.GetComponent<PlayerSetup>();
                playerSetup?.SetupCamera();
            });

            Debug.Log($"Player {playerRef} đã được hồi sinh!");
        }
    }
}
