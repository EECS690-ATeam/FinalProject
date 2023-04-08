using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoPing : MonoBehaviour
{
    public float fadeSpeed = 100f;
    public float fadeTime = 1f;

    private float spawnTime;
    private SpriteRenderer sprite;
    private Color tmpColor;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        sprite = GetComponent<SpriteRenderer>();
        tmpColor = sprite.color;
        sprite.color = tmpColor;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FadeOut());
        if (Time.time - spawnTime >= 1)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FadeOut()
    {
        tmpColor.a -= .01f;
        Debug.Log(Time.deltaTime);
        sprite.color = tmpColor;
        yield return null;
    }
}
