using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashlightAnimation : MonoBehaviour
{
    /*
    Retool of a script from:
    https://gist.github.com/almirage/e9e4f447190371ee6ce9
    */
    public Sprite[] sprites;
    public int framePerSprite;
    public bool loop;
    public bool empty;

    public int index = 0;
    private Image image;
    private int frame = 0;
    private int refillAmt = 3;
    private FieldOfView fov;

    void Awake()
    {
        image = GetComponent<Image>();
        fov = FindObjectOfType<FieldOfView>();
    }

    void Update()
    {
        if (index >= (sprites.Length - 1))
        {
            empty = true;
            return;
        }
        empty = false;
        frame++;
        if (frame < framePerSprite) return;
        index++;
        image.sprite = sprites[index];
        frame = 0;
        if (index >= sprites.Length)
        {
            if (loop) index = 0;
        }
    }

    public void batteryCollision()
    {
        Debug.Log("Hit a battery");
        if (empty)
        {
            empty = false;
            fov.pubSetActive();
        }
        if (index >= refillAmt)
        {
            index -= refillAmt;
            image.sprite = sprites[index];
            frame = 0;
        }
        else
        {
            index = 0;
            image.sprite = sprites[index];
        }
    }
}