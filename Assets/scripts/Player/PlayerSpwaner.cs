using UnityEngine;
using Fusion;
using System.Linq;
using UnityEngine.UI;

public class PlayerSpwaner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public RoomManager roomManager;
    public int maxplayer = 2;

    public GameObject nameInputPanel;  // Panel chứa UI nhập tên
    public TMPro.TMP_InputField nameInputField; // InputField để người chơi nhập tên
                                                // public TMPro.TextMeshProUGUI playerNameText;  // Text để hiển thị tên người chơi
    public Button confirmNameButton;


    private PlayerProperties localPlayerProps;
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


        if (RoomManager.IsGameStarted == false && Runner.ActivePlayers.Count() <= maxplayer && player == Runner.LocalPlayer)
        {
            Runner.Spawn(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity,
            Runner.LocalPlayer, (runner, obj) =>
            {
                var _player = obj.GetComponent<PlayerSetup>();
                _player.SetupCamera();

                localPlayerProps = obj.GetComponent<PlayerProperties>();

                // Bật UI nhập tên
                nameInputPanel.SetActive(true);
                confirmNameButton.onClick.RemoveAllListeners();
                // Xóa tất cả các listener trước đó để tránh xung đột`
                confirmNameButton.onClick.AddListener(() =>
                {
                    string name = nameInputField.text;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        localPlayerProps.SetPlayerName(name);
                        nameInputPanel.SetActive(false);


                    }
                });
            }
        );
        }
    }

}

