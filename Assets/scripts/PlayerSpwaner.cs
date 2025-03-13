using UnityEngine;
using Fusion;

public class PlayerSpwaner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {

    }
}