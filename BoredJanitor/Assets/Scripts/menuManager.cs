using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Canvas ui;
    [SerializeField] TMPro.TMP_Text boredMenuMeanComment;
    string[] meanComments = { "Womp Womp", "Cant even clean", "you suck", "-1 viewer", "Even your mom left the chat" };
    GameObject boredMenu;
    void Start()
    {
        boredMenu = ui.gameObject.transform.Find("boredMenu").gameObject;
    }
    public void showBoredMenu()
    {
        boredMenu.SetActive(true);
        string comment = meanComments[Random.Range(0, meanComments.Length - 1)];
        boredMenuMeanComment.SetText(comment);
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("YOOO shouldnt see this");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
