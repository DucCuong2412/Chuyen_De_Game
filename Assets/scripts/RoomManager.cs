using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnStartGameChanged))]
    public NetworkBool GameStarted { get; set; }

    [Networked] public NetworkBool isActiveTime { get; set; }
    public static bool IsGameStarted = false;


    public int playerCount { get; set; }

    public TextMeshProUGUI countdownText;
    private float countdownTime = 3f;

    [SerializeField] private GameObject startPanel;
    public GameObject button, img;
    public GameObject object1, object2;

    public GameObject winPanel;  // WinPanel để kéo thả vào trong Inspector

    public GameObject deadPanel; // DeadPanel để hiển thị khi chết

    public override void Spawned()
    {
        startPanel.SetActive(!GameStarted);
        countdownText.gameObject.SetActive(!isActiveTime);
        IsGameStarted = GameStarted;

        // Nếu có InputAuthority thì ẩn WinPanel khi game bắt đầu
        if (Object.HasInputAuthority && winPanel != null)
        {
            winPanel.SetActive(false); // Ẩn WinPanel lúc bắt đầu
        }

        // Nếu có InputAuthority thì ẩn DeadPanel khi game bắt đầu
        if (Object.HasInputAuthority && deadPanel != null)
        {
            deadPanel.SetActive(false); // Ẩn DeadPanel lúc mới sinh
        }
    }

    private void Start()
    {
        if (Object.HasInputAuthority && winPanel != null)
        {
            winPanel.SetActive(false); // Ẩn WinPanel lúc bắt đầu
        }

        // Ẩn DeadPanel khi bắt đầu game
        if (Object.HasInputAuthority && deadPanel != null)
        {
            deadPanel.SetActive(false); // Ẩn DeadPanel lúc mới sinh
        }
    }

    private void OnStartGameChanged()
    {
        IsGameStarted = GameStarted;
        button.SetActive(!GameStarted);
        countdownText.gameObject.SetActive(!isActiveTime);
        Debug.Log("Game Started Changed → " + GameStarted);
    }

    private void Update()
    {
        UpdatePlayerCount();

        // Kiểm tra sau khi game bắt đầu nếu chỉ còn 1 người sống
        if (GameStarted && !isActiveTime)
        {
            CheckWinCondition();
        }
    }

    public void UpdatePlayerCount()
    {
        playerCount = Runner.ActivePlayers.Count();//mỗi người join vào game sẽ tự động  cộng  id người chơi đó lên 1 đơn v
        Debug.Log($"Player count: {playerCount}");
    }

    // Kiểm tra điều kiện thắng khi chỉ còn 1 người sống
    private void CheckWinCondition()
    {
        var allPlayers = FindObjectsOfType<PlayerProperties>();
        int alivePlayers = 0;
        PlayerProperties winner = null;


        foreach (var player in allPlayers)
        {
            if (player != null && player.health > 0)
            {
                alivePlayers++;
                winner = player;  // Cập nhật người sống cuối cùng

            }
        }

        // Nếu chỉ còn 1 người sống, hiển thị winPanel
        if (alivePlayers == 1)
        {
            if (winPanel != null)
            {
                if (winner != null && winner.HasInputAuthority)
                {
                    winPanel.SetActive(true); // Chỉ hiển thị winPanel cho người chơi còn sống và có quyền điều khiển
                    Debug.Log("Một người còn sống, hiển thị WinPanel!");

                }
            }
        }
    }

    [SerializeField] private int maxPlayers = 2; // chỉnh được trong Unity

    public void StartGame()
    {
        if (HasStateAuthority && Runner.LocalPlayer.PlayerId == 1)
        {
            if (playerCount >= maxPlayers)
            {
                RpcStartCountdown();
                GameStarted = true;
                Debug.Log("Host bắt đầu game.");
            }
            else
            {
                Debug.Log("Chưa đủ người chơi để bắt đầu game.");
            }
        }
    }

    public IEnumerator DelayToStart()
    {
        Time.timeScale = 0;
        countdownText.gameObject.SetActive(true);

        float timeLeft = countdownTime;
        while (timeLeft > 0)
        {
            countdownText.text = Mathf.Ceil(timeLeft).ToString();
            timeLeft -= Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1;
        countdownText.gameObject.SetActive(false);
        img.SetActive(false);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcStartCountdown()
    {
        StartCoroutine(DelayToStart());
    }
}
