using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] string Type;
    [SerializeField] GameObject hudcanvas;
    void Start()
    {
        if (hudcanvas != null) {
            //hudcanvas.SetActive(false);
            //FindGameObjectsWithTag("Player")[0].GetComponent<PlayerManager>().s
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Type == "EnableHud") {
            hudcanvas.SetActive(true);
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerManager>().motivation = 50;
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<UiManager>().updateMotivation(50,true);
        }
    }
}
