using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatUI : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button SendButton;
    public TextMeshProUGUI ChatContent;
    private void Start()
    {
        SendButton.onClick.AddListener(SendMessage);
        
    }
    private void SendMessage()
    {
        string message=inputField.text;
        if(!string.IsNullOrEmpty(message))
        {
            //gọi chat managager
            ChatManager.instance.SendChatMessage(message);
            inputField.text = "";//xóa nội dung khi đã được gửi đi

        }
    }
}
