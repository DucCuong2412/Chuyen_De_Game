using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

using UnityEngine;

public class RoomManager : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnStartGameChanged))]
    public NetworkBool GameStarted { get; set; }
    [Networked] public NetworkBool isActiveTime { get; set; }

    public int playerCount { get; set; }
    public int paxPlayerCount { get; set; }
    public TextMeshProUGUI countdownText;
    // Tham chiếu đến UI Text để hiển thị đếm ngược
    private float countdownTime = 3f; // Thời gian đếm ngược
    private bool countdownActive = false;

    [SerializeField] private GameObject startPanel;
    public GameObject button, img;

    public GameObject object1, object2;

    public PlayerMovement player;

    [Networked]
    public bool isStartGame { get; set; }

    //public bool isStartGame = false;

    public void isStartGameA() {  }
    private void Start()
    {
        isStartGame = false;

    }
    private void Update()
    {

        UpdatePlayerCount();
        var player = FindAnyObjectByType<PlayerMovement>();

    }
    public override void Spawned()
    {
        startPanel.SetActive(!GameStarted);
        countdownText.gameObject.SetActive(!isActiveTime);

    }
    private void OnStartGameChanged()
    {
        //  startPanel.SetActive(!GameStarted);
        button.SetActive(!GameStarted);
        countdownText.gameObject.SetActive(!isActiveTime);
        Debug.Log("Game Started Changed → " + GameStarted);
        isStartGame = true;

        Runner.Spawn(object1, new Vector3(Random.Range(1, 3), 0, Random.Range(1, 3)));
        Runner.Spawn(object2, new Vector3(Random.Range(1, 3), 0, Random.Range(1, 3)));






    }

    public void UpdatePlayerCount()
    {
        playerCount = Runner.ActivePlayers.Count();
        Debug.Log($"Player count: {playerCount}");
    }
    public void StartGame()
    {
        if (HasStateAuthority && Runner.LocalPlayer.PlayerId == 1)
        {
            if (playerCount <= 2)
            {
                RpcStartCountdown();
                GameStarted = true;

                Debug.Log(Runner.LocalPlayer.PlayerId + "This is a Host");
            }
        }
        else
        {
            Debug.Log(Runner.LocalPlayer.PlayerId + "This is not Host");
        }
    }

    public IEnumerator DelayToStart()
    {
        Time.timeScale = 0;
        countdownText.gameObject.SetActive(true);

        while (countdownTime > 0)
        {
            countdownText.text = Mathf.Ceil(countdownTime).ToString(); // Hiển thị thời gian còn lại
            countdownTime -= Time.unscaledDeltaTime;
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
