using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    private int framecount;
    private bool on;
    // Start is called before the first frame update
    void Start()
    {
        on = false;
    }

    // Update is called once per frame
    void Update()
    {
        framecount += 1;
        if (framecount % 240 == 0) {
            if (!on) {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                on = true;
            }
            else {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                on = false;
            }
        }
    }
}
