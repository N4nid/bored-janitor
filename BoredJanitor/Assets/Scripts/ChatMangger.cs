using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChatMangger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject chatMessagePrefab;
    [SerializeField] GameObject chatView;
    //[SerializeField] float maxWidth = 450;
    [SerializeField] float xMargin = 245f;
    [SerializeField] float startY = 30f;
    [SerializeField] float yIncrement = 50f;
    List<GameObject> messages;
    GameObject player;
    [SerializeField] float viewHeight = 290;
    //int increment = 0;
    int prevLineCount;
    float currentY;
    string[] usernames = {"Soliy", "MortySmith", "Nanid","Aurelia","Elonita_Express","zellis","Alica","isTrue","Semilocon"
    ,"Jolpflgivck","TheLordGray","WrenGreen09","cripton86","fishNChips","DizzyRat","TeratoJaron","xXGamer420Xx","ameliaaa"
    ,"janitorDestoryer","cameleon","americanIdiot","totallyAwezome","Paul","Helena","FlowerLover","GrassToucher","Curby86"};
    string[] neutralMessages = {"https://www.youtube.com/watch?v=dQw4w9WgXcQ","Hi!","What's your OF?","odsjgkr","Anyone here in 2025?"
    ,"I am so tired","BeepBoop","Yooo","What's happening?","I am a big fan of your!","LOL","Pogies...","Pog!"};
    string[] positiveMessages = {"You rock!","That's crazy!","You totally rock that broom!","Hot!","Arr!","Slay!","wow","Keep it up", "Keep up the good work", "Noiice!","That shit's dingo!","raad!","You so cool!"};
    string[] negativeMessages = {"You serius?","You should hit the gym more often!","Your mama must be really fat!","You suck!", "What the heck man, hurry up!","zzz","so booring!","boo!",};
    void Start()
    {
        messages = new List<GameObject>();
        currentY = startY;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        Invoke("sendRandomMessage",Random.Range(0.5f,2f));
        //sendMessage("ks","öskjgflkshjf");
        //sendMessage("ks","öskjgflkshjf");
    }

    void sendRandomMessage() {
        int motivation = (int)player.GetComponent<PlayerManager>().motivation;
        string username = usernames[Random.Range(0,usernames.Length-1)];
        int isNeutralInt = Random.Range(0,3);
        string message = "";
        if (isNeutralInt == 0) {
            message = neutralMessages[Random.Range(0,neutralMessages.Length)];
        }
        else{
            int positiveInt = Random.Range(0,100);
            if (positiveInt<=motivation) {
                message = positiveMessages[Random.Range(0,positiveMessages.Length)];
            }
            else{
                message = negativeMessages[Random.Range(0,negativeMessages.Length)];
            }
        }
        sendMessage(username,message);
        Invoke("sendRandomMessage",Random.Range(0.5f,2f));
    }

    void sendMessage(string username, string message) {
        GameObject intanceMessagePrefab = Instantiate(chatMessagePrefab);
        int lines = intanceMessagePrefab.GetComponent<ChatMessagePrefab>().setMessage(username + ":",message);
        currentY += yIncrement * lines;
        intanceMessagePrefab.transform.SetParent(chatView.transform);
        if (currentY > viewHeight / 2) {
            prevLineCount = messages[0].gameObject.GetComponent<ChatMessagePrefab>().lineCount;
            currentY -= yIncrement * messages[0].gameObject.GetComponent<ChatMessagePrefab>().lineCount;
            Destroy(messages[0].gameObject);
            messages.RemoveAt(0);
            scroll();
        }
        intanceMessagePrefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(xMargin,currentY);
        messages.Add(intanceMessagePrefab);
    }
    void scroll() {
        for (int i = 0; i < messages.Count; i++) {
            messages[i].gameObject.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0,yIncrement * prevLineCount);
        }
    }
}
