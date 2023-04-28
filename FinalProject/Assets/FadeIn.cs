using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    private SpriteRenderer sr;
    public static float current;
    public static float goal;
    public static bool finalFade;
    //true == right, false == left
    public static bool direction;
    private float fadeRate;
    // Start is called before the first frame update
    void Start()
    {
        current = 1f;
        goal = 0f;
        sr = GetComponent<SpriteRenderer>();
        fadeRate = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        current = Mathf.MoveTowards( current, goal, fadeRate * Time.deltaTime);
        sr.color = new Color(0f, 0f, 0f, current);
        if (goal == 1f && current == 1f) {
            if (finalFade) {
                finalFade = false;
                SceneManager.LoadScene("Start Menu");
            }
            else {
                if (direction) {
                    FindObjectOfType<RightExit>().Transition();
                }
                else {
                    FindObjectOfType<LeftExit>().Transition();
                }
            }
        }
        if (goal == 0f && current == 0f && finalFade) {
            current= 0f;
            goal = 1f;
            fadeRate = 0.03f;
        }
    }
}
