using UnityEngine;

public class soundManager : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip killEffect;
    [SerializeField] float killEffectVolume = 1f;
    [SerializeField] AudioClip damageEffect;
    [SerializeField] float damageEffectVolume = 1f;
    [SerializeField] AudioClip broomLight;
    [SerializeField] float broomLightVolume = 1f;
    [SerializeField] AudioClip broomHeavy;
    [SerializeField] float broomHeavyVolume = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void playSound(string sfx)
    {
        switch (sfx)
        {
            case "killEffect":
                sound.volume = killEffectVolume;
                sound.PlayOneShot(killEffect);
                break;

            case "damageEffect":
                sound.volume = damageEffectVolume;

                sound.PlayOneShot(damageEffect);
                break;

            case "BroomLight":
                sound.volume = broomLightVolume;
                sound.PlayOneShot(broomLight);
                break;

            case "BroomHeavy":
                sound.volume = broomHeavyVolume;
                sound.PlayOneShot(broomHeavy);
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
