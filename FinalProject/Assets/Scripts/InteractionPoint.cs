using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPoint : MonoBehaviour
{
    public string message1;
    public string message2;
    public string message3;
    public string message4;
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
        distance = 7;
        if (isStory) {
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 140, 0, 255);
            this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(255, 140, 0, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && !beenSeen) {
            float diff = Vector3.Distance(this.transform.position, p.position);
            if (diff < distance) {
                a.Play(0);
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                Destroy(GetComponent<BoxCollider2D>());
                beenSeen = true;
                StartCoroutine(adjustMessage());
            }
        }

    }

    IEnumerator adjustMessage() {
        if (message1 != "") {
            Debug.Log("Message 1");
            thought.text = message1;
            yield return new WaitForSeconds(6);
        }
        if (message2 != "") {
            Debug.Log("Message 2");
            thought.text = message2;
            yield return new WaitForSeconds(6);
        }
        if (message3 != "") {
            Debug.Log("Message 3");
            thought.text = message3;
            yield return new WaitForSeconds(6);
        }
        if (message4 != "") {
            Debug.Log("Message 4");
            thought.text = message4;
            yield return new WaitForSeconds(6);
        }
        thought.text = "";
    }
}
