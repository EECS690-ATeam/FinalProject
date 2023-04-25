using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFade : MonoBehaviour
{
    private float currentFade;

    public float fadeAmplitude = 1f;
    public float fadeFrequency = 0.5f;
    public SpriteRenderer lightSR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentFade = (float)Mathf.Sin(Time.time * fadeFrequency) * fadeAmplitude;

        lightSR.material.SetColor("_Color", new Color(Mathf.Clamp(Mathf.Abs(currentFade) * 1.2f, 0.5f, 1.2f), Mathf.Clamp(Mathf.Abs(currentFade) * 1.2f, 0.5f, 1.2f), Mathf.Clamp(Mathf.Abs(currentFade) * 1.2f, 0.5f, 1.2f), Mathf.Clamp(Mathf.Abs(currentFade), 0.3f, 1f)));
    }
}
