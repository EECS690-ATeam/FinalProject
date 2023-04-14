using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPoint : MonoBehaviour
{
    public string message;
    public TMP_Text thought;
    public bool isStory;
    public Transform p;
    private bool beenSeen;
    private int distance;
    private AudioSource a;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<AudioSource>();
        beenSeen = false;
        distance = 10;
        if (isStory) {
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 140, 0, 255);
            this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(255, 140, 0, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && !beenSeen) {
            a.Play(0);
            float diff = Vector3.Distance(this.transform.position, p.position);
            if (diff < distance) {
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                beenSeen = true;
                StartCoroutine(adjustMessage());
            }
        }

    }

    IEnumerator adjustMessage() {
        thought.text = message;
        yield return new WaitForSeconds(5);
        thought.text = "";
    }
}
