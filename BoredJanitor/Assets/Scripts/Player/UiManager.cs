using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text motivationText;
    [SerializeField] Image motivationIndicator;
    [SerializeField] Animator boredMenuAnim;
    [SerializeField] Sprite ball100;
    [SerializeField] Sprite ball80;
    [SerializeField] Sprite ball60;
    [SerializeField] Sprite ball40;
    [SerializeField] Sprite ball20;
    [SerializeField] Sprite ball0;
    [SerializeField] float biggerScaleChange = 0.5f;
    [SerializeField] float smallerScaleChange = 0.3f;
    [SerializeField] Canvas ui;
    public GameObject boredMenu;
    public GameObject hud;


    void Start()
    {
        hud = ui.gameObject.transform.Find("HUD").gameObject;
        boredMenu = ui.gameObject.transform.Find("boredMenu").gameObject;
    }

    public void showBoredMenu()
    {
        hud.SetActive(false);
        boredMenuAnim.SetBool("fadeOut", false);
    }

    public void setMotivationText(string text)
    {
        motivationText.SetText(text);
    }

    public void makeBigger()
    {
        motivationIndicator.transform.localScale += new Vector3(smallerScaleChange, smallerScaleChange, smallerScaleChange);
    }
    public void makeSmaller()
    {
        motivationIndicator.transform.localScale += new Vector3(-biggerScaleChange, -biggerScaleChange, -biggerScaleChange);
    }

    public void updateMotivation(float motivation, bool bigger)
    {
        Debug.Log("bsjklfbs");
        if (bigger)
        {
            Invoke("makeSmaller", 0.3f);
            motivationIndicator.transform.localScale += new Vector3(biggerScaleChange, biggerScaleChange, biggerScaleChange);
        }
        else
        {
            Invoke("makeBigger", 0.2f);
            motivationIndicator.transform.localScale += new Vector3(-smallerScaleChange, -smallerScaleChange, -smallerScaleChange);

        }

        switch (motivation)
        {
            case > 80f:
                motivationIndicator.overrideSprite = ball100;
                break;
            case > 60f:
                motivationIndicator.overrideSprite = ball80;
                break;
            case > 40f:
                motivationIndicator.overrideSprite = ball60;
                break;
            case > 20f:
                motivationIndicator.overrideSprite = ball40;
                break;
            case > 0f:
                motivationIndicator.overrideSprite = ball20;
                break;
            case <= 0f:
                motivationIndicator.overrideSprite = ball0;
                break;

            default:
                motivationIndicator.overrideSprite = ball100;
                break;
        }
         setMotivationText("Motivtion: " + (int)motivation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
