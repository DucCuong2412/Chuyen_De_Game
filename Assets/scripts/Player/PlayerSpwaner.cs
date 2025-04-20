using UnityEngine;
using Fusion;
using System.Linq;

public class PlayerSpwaner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public RoomManager roomManager;
    public int maxplayer = 2;

    private void Start()
    {
        roomManager = FindAnyObjectByType<RoomManager>();
    }
    public void PlayerJoined(PlayerRef player)
    {
        if (RoomManager.IsGameStarted)
        {
            Debug.Log("Không spawn - Game đã bắt đầu.");
            return;
        }
        if (Runner.ActivePlayers.Count() > maxplayer)
        {
            Debug.Log("Không spawn - Game đã đủ người.");
            return;

        }


        if(RoomManager.IsGameStarted==false&& Runner.ActivePlayers.Count() <= maxplayer)
        {
            Runner.Spawn(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity,
            Runner.LocalPlayer, (runner, obj) =>
            {
                var _player = obj.GetComponent<PlayerSetup>();
                _player.SetupCamera();
            }
        );
        }
    }

}

