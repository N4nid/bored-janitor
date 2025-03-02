using System;
using TMPro;
using UnityEngine;

public class ChatMessagePrefab : MonoBehaviour
{
    [SerializeField] GameObject usernameTextObj;
    [SerializeField] GameObject messageTextObj;
    TMP_Text usernameText;
    TMP_Text chatMessage;
    [SerializeField] float charWidth = 19f;
    [SerializeField] float xOffset = 5f;
    RectTransform usernameTrans;
    RectTransform messageTrans;
    void Awake()
    {
        usernameText = usernameTextObj.GetComponent<TMP_Text>();
        chatMessage = messageTextObj.GetComponent<TMP_Text>();
        usernameTrans = usernameTextObj.GetComponent<RectTransform>();
        messageTrans = usernameTextObj.GetComponent<RectTransform>();
    }

    public void setMessage(String username, String message) {
        float usernameWidth = username.Length * charWidth;
        float chatWidth = message.Length * charWidth;
        usernameTrans.anchoredPosition = new Vector2(usernameWidth/2 + xOffset,0f);
        usernameTrans.sizeDelta = new Vector2(usernameWidth,34f);
        messageTrans.anchoredPosition = new Vector2(usernameWidth + 2*xOffset+chatWidth/2,0f);
        messageTrans.sizeDelta = new Vector2(chatWidth,34f);
        usernameText.text = username;
        chatMessage.text = message;
    }

}
