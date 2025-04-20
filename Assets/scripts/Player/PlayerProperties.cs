using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerProperties : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public int health { get; set; }

    [Networked, OnChangedRender(nameof(OnPlayerNameChanged))]
    public string playerName { get; set; }

    public TextMeshProUGUI playerNameText;

    public Slider Slider;
    private GameObject deadPanel;

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetPlayerName(string name)
    {
        playerName = name;
    }
    private void OnPlayerNameChanged()
    {
        if (playerNameText != null)
        {
            playerNameText.text = playerName;
            Debug.Log($"Tên đã được cập nhật: {playerName}");
        }
    }


    public void SetPlayerName(string name)
    {
        if (HasInputAuthority) // Chỉ người chơi sở hữu đối tượng mới có thể thay đổi tên
        {
            playerName = name;
            if (playerNameText != null)
            {
                playerNameText.text = playerName;

            }
        }
    }
    public override void FixedUpdateNetwork()
    {
        if (!HasInputAuthority) return;


        playerNameText.text = playerName;

    }

















    private void OnHealthChanged()
    {
        Slider.value = health;
        Debug.Log($" heath changed to {health}");
        if (health <= 0)
        {
            Debug.Log("player is dead");
        }
    }
    private void Start()
    {
        health = 100;
        Slider.value = health;
        GameObject panel = GameObject.FindGameObjectWithTag("DeadPanel");
        if (panel != null)
        {
            deadPanel = panel;
            deadPanel.SetActive(false); // Ẩn lúc mới spawn
            Debug.Log("Dead panel found and set to inactive.==========");
        }
        else
        {
            Debug.Log("11111111nullllllllllll==========");
        }
    }
    void Update()
    {
        if (HasInputAuthority)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                health -= 10;
            }

            if (health <= 0)
            {


                if (deadPanel != null)
                {

                    deadPanel.SetActive(true); // Ẩn lúc mới spawn
                    Debug.Log("Dead panel found and set to inactive.==========");

                }
                else
                {
                    Debug.Log("Dnullllllllllll==========");
                }
                takeDie();
            }



        }





    }
    private void OnTriggerEnter(Collider other)
    {
        if (Object.HasInputAuthority)
        {
            if (other.gameObject.CompareTag("at"))
            {
                // var targetPlayer = other.gameObject.GetComponent<NetworkObject>().InputAuthority;
                //TakeDamege(10);
                health -= 10;

                Debug.Log("heal-=10");
            }

        }

    }



    public IEnumerator die()
    {
        Debug.Log("animation die Is on===========");

        yield return new WaitForSeconds(3);

        // Tìm player khác còn sống
        var other = FindOtherAlivePlayer();
        if (other != null)
        {
            Debug.Log("Switching camera to another alive player");

            // Lấy CameraFlow
            var camFlow = FindFirstObjectByType<CameraFlow>();
            if (camFlow != null)
            {
                camFlow.AsighCamera(other.transform);
                ///cho gán mỗi cam freelock thoi

                camFlow.AsighCamera2(other.transform);
                camFlow.AsighCamera3(other.transform);

            }
        }
        else
        {
            Debug.Log("chả làm gì cả vì không tìm thấy player khác");
        }

        Destroy(gameObject);
    }


    private PlayerProperties FindOtherAlivePlayer()
    {
        var all = FindObjectsOfType<PlayerProperties>();

        foreach (var p in all)
        {
            if (p != this && p.health > 0)
            {
                return p;
            }
        }

        return null;
    }
    public void TakeDamege(int Damage)
    {
        if (HasInputAuthority)
        {
            health = Mathf.Max(0, health - Damage);
        }
    }


    public void takeDie()
    {
        StartCoroutine(die());

    }
}
