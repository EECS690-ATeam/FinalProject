using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private SpriteRenderer sr;
    private Color onColor;
    private Color offColor;

    // Start is called before the first frame update
    void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();
        onColor = new Color(1f, 1f, 1f, 1f);
        offColor = new Color(1f, 1f, 1f, 0f);
        sr.color = offColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < 10f)
        {
            sr.color = onColor;
        }
        else
        {
            sr.color = offColor;
        }
    }
}
