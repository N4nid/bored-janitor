using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject player;
    [SerializeField] int LevelToUnlock;
    public float motivation = 100; // acts as Health
    [SerializeField] UiManager ui;
    [SerializeField] GameObject thanks;
    [SerializeField] soundManager sound;
    [SerializeField] Vector3 spawnPoint = new Vector3(0, 0, 0);
    public bool isBored = false;
    public bool isInvincible = false;

    string motivationText = "Motivation: ";
    //int score = 0;
    public int killCounter = 0; // changed in Eneymmanager

    void Start()
    {

        ui.setMotivationText(motivationText + motivation);
        if (!isInvincible)
        {
            Invoke("timer", 1);
        }
    }

    public void timer()
    {
        loseMotivation(3);
        if (motivation > 0 && !isInvincible)
        {
            Invoke("timer", 1);
        }
    }



    public void gainMotivation(float amount)
    {
        motivation += amount;
        ui.setMotivationText(motivationText + motivation);
        ui.updateMotivation(motivation, true);
        sound.damagePitchOffset = 0.5f - (motivation / 200f);
        sound.playSound("killEffect");
    }

    public void loseMotivation(float amount)
    {
        if (!isBored && !isInvincible)
        {
            motivation -= amount;
            ui.setMotivationText(motivationText + motivation);
            ui.updateMotivation(motivation, false);
            sound.damagePitchOffset = 0.5f - (motivation / 200f);
            sound.playSound("damageEffect");
            if (motivation <= 0)
            {
                becomeBored();
            }

        }

    }

    void becomeBored()
    {
        ui.setMotivationText("Im bored, yall suck");
        isBored = true;
        ui.showBoredMenu();
        player.GetComponent<Animator>().SetBool("isBored", true);
        sound.playMusic("boredBgMusic");
        //TODO make die
    }

    void finish()
    {
        if (LevelToUnlock != 3) {
            thanks.SetActive(false);
        }
        PlayerPrefs.SetInt("levelsUnlocked",LevelToUnlock);
        isInvincible = true;
        ui.showWinScreen();
        sound.playMusic("winBgMusic");

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Finish"))
        {
            Debug.Log("Finnished");
            finish();

        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
