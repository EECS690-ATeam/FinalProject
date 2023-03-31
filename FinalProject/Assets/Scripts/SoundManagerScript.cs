using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip clickSound;

    static AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        clickSound = Resources.Load<AudioClip>("click_2_");
    }

    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "click_2_":
                audioSrc.PlayOneShot(clickSound);
                break;
        }
    }
}
