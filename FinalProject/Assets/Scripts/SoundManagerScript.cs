using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip clickSound;
    public static AudioClip stingerSound;

    static AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        clickSound = Resources.Load<AudioClip>("click_2_");
        stingerSound = Resources.Load<AudioClip>("stinger");
    }

    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "click_2_":
                audioSrc.PlayOneShot(clickSound, 0.2f);
                break;

            case "stinger":
                audioSrc.PlayOneShot(stingerSound, 0.1f);
                break;
        }
    }
}
