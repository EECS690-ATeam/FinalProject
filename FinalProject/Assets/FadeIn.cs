using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private SpriteRenderer sr;
    public static float current;
    public static float goal;
    // Start is called before the first frame update
    void Start()
    {
        current = 1f;
        goal = 0f;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        current = Mathf.MoveTowards( current, goal, 0.9f * Time.deltaTime);
        sr.color = new Color(0f, 0f, 0f, current);
        if (goal == 1f && current == 1f) {
            FindObjectOfType<ExitPrompt>().Transition();
        }
    }
}
