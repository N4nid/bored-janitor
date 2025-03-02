using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject player;
    [SerializeField] float motivation = 100; // acts as Health
    [SerializeField] UiManager ui;
    string motivationText = "Motivation: ";
    int score = 0;
    public int killCounter = 0; // changed in Eneymmanager

    void Start()
    {

        ui.setMotivationText(motivationText + motivation);
        Invoke("timer", 1);
    }

    void timer()
    {
        loseMotivation(10);
        if (motivation > 0)
        {
            Invoke("timer", 1);
        }
    }


    public void gainMotivation(float amount)
    {
        motivation += amount;
        ui.setMotivationText(motivationText + motivation);
        ui.updateMotivation(motivation, true);
    }

    public void loseMotivation(float amount)
    {
        motivation -= amount;
        ui.setMotivationText(motivationText + motivation);
        ui.updateMotivation(motivation, false);
        if (motivation <= 0)
        {
            becomeBored();
        }

    }

    void becomeBored()
    {
        ui.setMotivationText("Im bored, yall suck");
        //TODO make die
    }

    void finish()
    {
        //TODO make finish
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Finish"))
        {
            Debug.Log(collision.gameObject.name);
            finish();

        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
