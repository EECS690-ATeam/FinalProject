using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractionPoint : MonoBehaviour, IDataPersistence
{
    [SerializeField] public string id;
    [ContextMenu("Create guid for id")]

    private void GenerateGiud()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private SpriteRenderer circle;
    private SpriteRenderer letter;
    public string message1;
    public string message2;
    public string message3;
    public string message4;
    public TMP_Text thought;
    public TMP_Text noteText;
    public Image noteOverlay;
    public GameObject closeNoteBtn;
    public bool isNote;
    public Transform p;
    public bool beenSeen;
    private int distance;
    private AudioSource a;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<AudioSource>();
        a.volume = 0.18F;
        beenSeen = false;
        if (SceneManager.GetActiveScene().name == "Lab2") {
            distance = 10;
        }
        else {
            distance = 5;
        }
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 150);
        this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        closeNoteBtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float diff = Vector3.Distance(this.transform.position, p.position);
        if (!beenSeen)
        {
            if (diff < distance)
            {
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 150);
            }
            else
            {
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 150);
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            }
        }
        else
        {
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
            
        if (Input.GetKeyDown("e") && !beenSeen) {
            if (diff < distance) {
                a.Play(0);
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                if (isNote) {
                    // StartCoroutine(noteMessage());
                    DisplayNote();
                }
                else {
                    beenSeen = true;
                    Destroy(GetComponent<BoxCollider2D>());
                    StartCoroutine(subtitleMessage());

                }
            }
        }

    }

    public void LoadData(GameData data)
    {
        data.ipsVisited.TryGetValue(id, out beenSeen);
        if (beenSeen)
        {
            this.gameObject.SetActive(false);
            // this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            // this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.ipsVisited.ContainsKey(id))
        {
            data.ipsVisited.Remove(id);
        }
        data.ipsVisited.Add(id, beenSeen);
    }

    IEnumerator subtitleMessage() {
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

    // IEnumerator noteMessage() {
    //     noteOverlay.color = new Color(1f, 1f, 1f, 1f);
    //     noteText.text = message1;
    //     // yield return new WaitForSeconds(10);
    //     noteText.text = "";
    //     noteOverlay.color = new Color(1f, 1f, 1f, 0f);
    // }

    public void DisplayNote()
    {
        noteOverlay.color = new Color(1f, 1f, 1f, 1f);
        noteText.text = message1;
        closeNoteBtn.SetActive(true);
    }

    public void CloseNote()
    {
        noteText.text = "";
        noteOverlay.color = new Color(1f, 1f, 1f, 0f);
        closeNoteBtn.SetActive(false);
    }
}
