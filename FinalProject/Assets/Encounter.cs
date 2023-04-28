using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Encounter : MonoBehaviour
{
    public TMP_Text thought;
    public TMP_Text SylasText;
    private bool started;
    private bool decision;
    private int waitCount;

    // For Save
    public string Scene = "";

    // Start is called before the first frame update
    void Start()
    {
        started = false;
        decision = false;
        waitCount = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) {
            started = true;
            StartCoroutine(EncounterText());
        }
        if (decision) {
            thought.text="Press 1 to find your way back, and 2 to press onward.";
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                decision = false;
                thought.text="";
                StartCoroutine(Option1());
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                thought.text="";
                decision = false;
                StartCoroutine(Option2());
            }
        }
    }

    IEnumerator EncounterText() {
        SylasText.text = "Oh dear, look what the cat dragged in. I was beginning to worry Blackwell had forgotten all about their mess down here.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text= "But where are my manners. Dr. Sylas Pratchett at your service, last remaining member of my team.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text="";
        thought.text= "... I certainly wasn't expecting to see anyone else down here, but you're more welcome company than the fish I've met so far.";
        yield return new WaitForSeconds(waitCount);
        thought.text="";
        SylasText.text= "I wouldn't be so hasty there boy, I know why they've sent you, I know all about Blackwell's little games.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text= "Do you play me for a fool? I know what you saw to get this far, you're here to sort through what's left and tie up loose ends.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text= "And to Blackwell, I am a loose end.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text="";
        thought.text= "Sir, please calm down, I'm not here to hurt you, all I was told was to come down here and find out what I could.";
        yield return new WaitForSeconds(waitCount);
        thought.text="";
        SylasText.text= "Oh don't kid yourself, you're expendable. They don't care about you OR what's down here, they want ME.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text= "All of my research, wasted on a company that would kill an ecosystem to keep the lights on.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text= "I'll give you the same choice I gave my team, although they chose rather poorly:";
        yield return new WaitForSeconds(waitCount);
        SylasText.text= "You can leave now, go back emptyhanded but alive, and rid yourself of this affront to nature.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text= "Or you can continue this foolishness, and experience the very nature I intend to unleash upon Blackwell.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text="";
        thought.text= "I won't take threats from a man who killed his own friends and colleagues.";
        yield return new WaitForSeconds(waitCount);
        thought.text="";
        SylasText.text= "Oh don't be silly, the Mother killed them, I just gave them an initial introduction... Make your choice.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text="";
        decision = true;
    }

    IEnumerator Option1() {
        SylasText.text= "I'm terribly sorry, I believe I've misled you. You've seen me and you've seen what has happened here.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text= "You cannot be allowed to live. I will not allow Blackwell to avoid facing the consequences of their actions.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text="";
        thought.text= "What actions?! They didn't kill your team, YOU did.";
        yield return new WaitForSeconds(waitCount);
        thought.text="";
        SylasText.text= "They're killing this planet, and I will use it to kill them. But first, I must allow it to kill you.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text= "Don't worry, trials have shown that it will be rather painless. It was lovely meeting you, what's your name? Oh, I suppose it doesn't matter now does it.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text="";
        FadeIn.current = 0f;
        FadeIn.goal = 1f;
        yield return new WaitForSeconds(2);
        PlayerMovement.labSpawn = 3;
        Scene = "Exterior Area";
        FadeIn.finalFade = true;
        SceneManager.LoadScene("Exterior Area");
    }

    IEnumerator Option2() {
        SylasText.text= "I can't say I don't admire your courage. It was rather nice meeting you, what's your name again?";
        yield return new WaitForSeconds(waitCount);
        SylasText.text="";
        thought.text= "It's Trevor, you --------";
        yield return new WaitForSeconds(waitCount);
        thought.text="";
        SylasText.text= "*Sylas laughs* If you do manage to make it past the Mother, I give you my word I will not pursue you. She's outside, please be on your way now.";
        yield return new WaitForSeconds(waitCount);
        SylasText.text="";
        FadeIn.current = 0f;
        FadeIn.goal = 1f;
        yield return new WaitForSeconds(2);
        PlayerMovement.labSpawn = 3;
        Scene = "Exterior Area";
        SceneManager.LoadScene("Exterior Area");
    }
}
