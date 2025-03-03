using TMPro;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] string Type;
    [SerializeField] GameObject hudcanvas;
    void Start()
    {
        if (hudcanvas != null) {
            hudcanvas.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Type == "EnableHud") {
            hudcanvas.SetActive(true);
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerManager>().motivation = 70;
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<UiManager>().updateMotivation(70,true);
        }
        if (Type == "DemotivatePlayer") {
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerManager>().isInvincible = false;
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerManager>().timer();
        }
    }
}
