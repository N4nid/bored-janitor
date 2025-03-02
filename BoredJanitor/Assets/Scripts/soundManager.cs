using UnityEngine;

public class soundManager : MonoBehaviour
{
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource music;
    [SerializeField] AudioClip killEffect;
    [SerializeField] float killEffectVolume = 1f;
    [SerializeField] AudioClip damageEffect;
    [SerializeField] float damageEffectVolume = 1f;
    public float damagePitchOffset = 0f;
    [SerializeField] AudioClip broomLight;
    [SerializeField] float broomLightVolume = 1f;
    [SerializeField] AudioClip broomHeavy;
    [SerializeField] float broomHeavyVolume = 1f;
    [SerializeField] AudioClip fastBgMusic;
    [SerializeField] float fastBgMusicVolume = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playMusic(fastBgMusic, fastBgMusicVolume, true);
    }

    public void playMusic(AudioClip audio, float volume, bool doLoop)
    {
        music.loop = doLoop;
        music.volume = volume;
        music.clip = audio;
        music.Play();

    }

    public void playSound(string sound)
    {
        GameObject clonedSound = GameObject.Instantiate(sfx.gameObject);
        clonedSound.name = "sfx:" + sound;
        AudioSource clonedSfx = clonedSound.GetComponent<AudioSource>();
        AudioClip toPlay = null;
        float volume = 1f;
        float pitch = 1f;

        switch (sound)
        {
            case "killEffect":
                volume = killEffectVolume;
                pitch = Random.Range(0.95f, 1.15f);
                toPlay = killEffect;
                break;

            case "damageEffect":
                volume = damageEffectVolume;
                toPlay = damageEffect;
                pitch = 1f - damagePitchOffset;
                break;

            case "BroomLight":
                volume = broomLightVolume;
                toPlay = broomLight;
                pitch = Random.Range(0.9f, 1f);
                break;

            case "BroomHeavy":
                volume = broomHeavyVolume;
                toPlay = broomHeavy;
                pitch = Random.Range(0.8f, 1.1f);
                break;

            default:
                GameObject.Destroy(clonedSound);
                break;
        }
        if (toPlay != null)
        {

            clonedSfx.pitch = pitch;
            clonedSfx.volume = volume;
            clonedSfx.PlayOneShot(toPlay);
            GameObject.Destroy(clonedSound, toPlay.length);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
