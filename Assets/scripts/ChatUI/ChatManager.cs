using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Rendering;

public class ChatManager : NetworkBehaviour
{
    public static ChatManager instance;
    private List<string> chatMessage= new List<string>();   
    public ChatUI ChatUI;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    [Rpc(RpcSources.All,RpcTargets.All)]
    public void RpcReceiveChatMessage(string playername,string message)
    {
        string formatedMessage=$"{playername} :{message}";   
        chatMessage.Add(formatedMessage);
        ChatUI.ChatContent.text += formatedMessage + "\n";

    }
    public void SendChatMessage(string message)
    {
        string playername=Runner.LocalPlayer.PlayerId.ToString();
        RpcReceiveChatMessage(playername,message);
        Debug.Log($"{playername}: {message}");
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
