using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RoomManager roomManager;

    private Button button;

    private void Start()
    {
        if (roomManager.Runner.LocalPlayer.PlayerId != 1)
        {
            gameObject.SetActive(false);
        }
    }


    private void loadComponent()
    {
        if (button != null) return;
        button = GetComponent<Button>();
        Debug.Log("Added button");
    }

    public void OnClickStart()
    {

        roomManager.StartGame();

    }
}
