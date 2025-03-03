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
        Debug.Log("IF boredMENU NOT there look in boredMenuImg in player");
        boredMenu.SetActive(true);
        string comment = meanComments[Random.Range(0, meanComments.Length - 1)];
        boredMenuMeanComment.SetText(comment);
    }
    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
