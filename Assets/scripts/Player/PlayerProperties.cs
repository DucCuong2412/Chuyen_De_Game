using Fusion;
using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.UI;


public class PlayerProperties : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public int health { get; set; } = 100;

    [Networked, OnChangedRender(nameof(OnPlayerNameChanged))]
    public string playerName { get; set; }


    [Networked]
    public int score { get; set; }




    public TextMeshProUGUI playerNameText;

    public Slider Slider;
    private GameObject deadPanel;


    private Respawn respawn;

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



    public void addScore(int amount)
    {
        if (HasInputAuthority) // Chỉ người chơi sở hữu đối tượng mới có thể thay đổi tên
        {
            score += amount;

        }
        else
        {
            RPC_addScore(amount);



        }


    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]// gọi host ra để cùng nhau đồng bộ
    void RPC_addScore(int amout)
    {
        score += amout;
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_RequestDespawn(NetworkId objectId)
    {
        NetworkObject obj = Runner.FindObject(objectId);
        if (obj != null)
        {
            gameObject.SetActive(false);
            Debug.Log("Đã ẩn obj");

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

                    respawn = GetComponent<Respawn>();
                    if (respawn != null)
                    {
                        

                        Debug.Log("player is dead=========== ddang chowf hooi sinh");
                    }
                    else
                    {
                        Debug.Log("player nullllll.");
                    }


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
            if (other.gameObject.CompareTag("Score"))
            {
                addScore(1);
                Debug.Log("score+=10");
                //NetworkObject scoreObj = other.GetComponent<NetworkObject>();
                //if (scoreObj != null)
                //{
                //    if (scoreObj.HasStateAuthority)
                //    {
                //        // Runner.Despawn(scoreObj); // Host có quyền thì xóa luôn

                //        gameObject.SetActive(false);
                //        Debug.Log("Host is despawning: " + scoreObj.name);
                //    }
                //    else
                //    {
                //        // Gửi NetworkId cho host yêu cầu xóa
                //        Debug.LogWarning("Không có quyền StateAuthority để xóa");
                //        RPC_RequestDespawn(scoreObj.Id);
                //    }
                //}

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

        // Destroy(gameObject);
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
        if (Object.HasStateAuthority) // Chỉ host mới được gọi Despawn
        {
            var respawnManager = FindObjectOfType<Respawn>();
            if (respawnManager != null)
            {
                respawnManager.RespawnPlayer(Object.InputAuthority);
            }

            Runner.Despawn(Object); // Despawn chính player object này
        }

        StartCoroutine(die());
    }
}
