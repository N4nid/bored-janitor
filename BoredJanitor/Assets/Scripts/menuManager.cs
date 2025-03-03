using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Canvas ui;
    [SerializeField] TMPro.TMP_Text boredMenuMeanComment;
    [SerializeField] GameObject levelButtons;
    string[] meanComments = { "Womp Womp", "Cant even clean", "you suck", "just sad", "Even your mom left the chat" };
    GameObject boredMenu;
    GameObject winScreen;

    void Start()
    {
        Debug.Log("wow on start");
        if (levelButtons != null)
        {
            Debug.Log("Lvl buttons are here");
            int levelsUnlocked = (PlayerPrefs.HasKey("levelsUnlocked")) ? (PlayerPrefs.GetInt("levelsUnlocked")) : 0;
            for (int i = 0; i < levelsUnlocked; i++)
            {
                GameObject lvlButton = levelButtons.transform.GetChild(i).gameObject;
                Debug.Log("Lvl buttons " + lvlButton.gameObject.name);
                if (lvlButton != null)
                {
                    lvlButton.SetActive(true);
                }
                else
                {
                    break;
                }

            }
        }
        else
        {
            boredMenu = ui.gameObject.transform.Find("boredMenu").gameObject;
            winScreen = ui.gameObject.transform.Find("winScreen").gameObject;
            Debug.Log("No lvl buttons here");
        }
    }


    public void showWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void showBoredMenu()
    {
        Debug.Log("IF boredMENU NOT there look in boredMenuImg in player");
        boredMenu.SetActive(true);
        string comment = meanComments[Random.Range(0, meanComments.Length - 1)];
        boredMenuMeanComment.SetText(comment);
    }

    public void nextLevel()
    {
        if (PlayerPrefs.HasKey("levelsUnlocked"))
            PlayerPrefs.SetInt("levelsUnlocked", PlayerPrefs.GetInt("levelsUnlocked") + 1);
        else
            PlayerPrefs.SetInt("levelsUnlocked", 0);

        Debug.Log("LvLs unlocked: " + PlayerPrefs.GetInt("levelsUnlocked"));

        PlayerPrefs.Save();
        goToMainMenu();
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
