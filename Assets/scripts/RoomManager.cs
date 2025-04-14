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

    public TextMeshProUGUI countdownText;
    private float countdownTime = 3f;

    [SerializeField] private GameObject startPanel;
    public GameObject button, img;
    public GameObject object1, object2;

    public override void Spawned()
    {
        startPanel.SetActive(!GameStarted);
        countdownText.gameObject.SetActive(!isActiveTime);
    }

    private void OnStartGameChanged()
    {
        button.SetActive(!GameStarted);
        countdownText.gameObject.SetActive(!isActiveTime);
        Debug.Log("Game Started Changed → " + GameStarted);

        Runner.Spawn(object1, new Vector3(Random.Range(1, 3), 0, Random.Range(1, 3)));
        Runner.Spawn(object2, new Vector3(Random.Range(1, 3), 0, Random.Range(1, 3)));
    }

    private void Update()
    {
        UpdatePlayerCount();
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
                Debug.Log(Runner.LocalPlayer.PlayerId + " is Host");
            }
        }
        else
        {
            Debug.Log(Runner.LocalPlayer.PlayerId + " is not Host");
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
