using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    private float fadeSpeed = 0.01f;
    private float growSpeed = 1.01f;
    private float travelSpeed = 0.19f;
    private float fadeTime = 6f;

    private float spawnTime;
    private SpriteRenderer sr;
    private Color tmpColor;
    private Vector3 tmpScale;
    private Vector3 tmpDirection;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        sr = GetComponent<SpriteRenderer>();
        tmpColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FadeOut());
        if (Time.time - spawnTime >= fadeTime)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FadeOut()
    {
        tmpDirection = transform.rotation * Vector3.up;
        transform.position += (tmpDirection * travelSpeed);
        //transform.Translate(tmpDirection * travelSpeed);
        transform.localScale *= growSpeed;
        tmpColor.a -= fadeSpeed;
        sr.color = tmpColor;
        yield return null; ;
    }
}
