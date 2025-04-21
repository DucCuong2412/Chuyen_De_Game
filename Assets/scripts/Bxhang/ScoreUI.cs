using Fusion;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreUI : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI topPlayerText;

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority)
        {
            UpdateTopPlayerAndSync();
        }
    }

    void UpdateTopPlayerAndSync()
    {
        var allPlayers = FindObjectsOfType<PlayerProperties>();

        if (allPlayers.Length == 0) return;

        int topScore = allPlayers.Max(p => p.score);
        var topNames = allPlayers
            .Where(p => p.score == topScore)
            .Select(p => p.playerName)
            .ToList();

        string names = string.Join(", ", topNames);

        // Gửi xuống tất cả client
        RPC_UpdateTopPlayerUI(names, topScore);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void RPC_UpdateTopPlayerUI(string playerNames, int score)
    {
        topPlayerText.text = $" <b>Top 1:{playerNames}:</b>  <color=red>{score}</color> ";
    }
}
