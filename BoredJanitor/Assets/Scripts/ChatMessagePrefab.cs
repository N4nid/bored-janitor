using System;
using TMPro;
using UnityEngine;

public class ChatMessagePrefab : MonoBehaviour
{
    [SerializeField] GameObject usernameTextObj;
    [SerializeField] GameObject messageTextObj;
    TMP_Text usernameText;
    TMP_Text chatMessage;
    [SerializeField] float charWidth = 25f;
    [SerializeField] float maxWidth = 450;
    [SerializeField] float xOffset = 5f;
    RectTransform usernameTrans;
    RectTransform messageTrans;
    public int lineCount;
    void Awake()
    {
        usernameText = usernameTextObj.GetComponent<TMP_Text>();
        chatMessage = messageTextObj.GetComponent<TMP_Text>();
        usernameTrans = usernameTextObj.GetComponent<RectTransform>();
        messageTrans = messageTextObj.GetComponent<RectTransform>();
    }

    public int setMessage(String username, String message) {
        float usernameWidth = (username.Length + 1)* charWidth;
        float chatWidth = message.Length * charWidth;
        usernameTrans.anchoredPosition = new Vector2(usernameWidth/2 + xOffset,0f);
        usernameTrans.sizeDelta = new Vector2(usernameWidth,34f);
        messageTrans.anchoredPosition = new Vector2(usernameWidth + xOffset+chatWidth/2,0f);
        messageTrans.sizeDelta = new Vector2(chatWidth,34f);
        usernameText.text = username;
        chatMessage.text = message;
        if (usernameWidth + chatWidth > maxWidth) {
            Debug.Log("Hi");
            messageTrans.sizeDelta = new Vector2(maxWidth,messageTrans.sizeDelta.y);
            messageTrans.anchoredPosition = new Vector2(maxWidth/2 + xOffset,0f);
            chatMessage.SetText("<space=" + usernameWidth + ">" + message);
            lineCount = 2;
            return 2;
        }
        lineCount = 1;
        return 1;
    }

}
