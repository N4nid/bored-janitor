using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChatMangger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject chatMessagePrefab;
    [SerializeField] GameObject chatView;
    [SerializeField] float xMargin = 245f;
    [SerializeField] float startY = 30f;
    [SerializeField] float yIncrement = 50f;
    List<GameObject> messages;
    int maxMessanges = 5;
    int increment = 0;
    void Start()
    {
        messages = new List<GameObject>();
    }

    void Update()
    {
        if (increment%100 == 0) {
            sendMessage("RandomAsssUsername","Hi, I am a big fan!");
        }
        increment++;
    } 
    void sendMessage(String username, String message) {
        GameObject intanceMessagePrefab = Instantiate(chatMessagePrefab);
        if (messages.Count >= maxMessanges) {
            GameObject.Destroy(messages[0]);
            messages.RemoveAt(0);
        }
        intanceMessagePrefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(xMargin,startY + messages.Count * yIncrement);
        intanceMessagePrefab.GetComponent<ChatMessagePrefab>().setMessage(username + ":",message);
        intanceMessagePrefab.transform.SetParent(chatView.transform);
    }
}
