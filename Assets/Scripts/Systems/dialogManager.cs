using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;


public class dialogManager : MonoBehaviour
{
    public static dialogManager instance;

    public bool showingDialog;
    public float dialogTimer;
    public dialogConversation currentConversation;
    public dialogLine currentLine;
    private int dialogIndex;

    public TextMeshProUGUI dialogDisplay;
    // Start is called before the first frame update
    void Start()
    {
        //singleton behavior
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showingDialog)
        {
            timer();
        }
    }

    public void startDialog(dialogConversation c)
    {
        currentConversation = c;
        dialogIndex = 0;
        currentLine = currentConversation.lines[dialogIndex];
        dialogTimer = 0;
        showingDialog = true;
        getLineLength();
        showDialog();
    }

    [Button()]
    public void debugDialog()
    {
        startDialog(currentConversation);
    }

    private void getLineLength()
    {
        if (currentLine.soundClip != null && !currentConversation.dontPlayAudio)
        {
            dialogTimer = currentLine.soundClip.length;
            currentLine.length = currentLine.soundClip.length;
        }
        else
        {
            dialogTimer = currentLine.length;
        }
    }

    private void showDialog()
    {
        //change this to take into account if subtitles are on
        dialogDisplay.color = Color.white;
        dialogDisplay.text = currentLine.dialog;
        //play the audio from the sound source
    }

    private void nextLine()
    {
        dialogIndex++;
        if (dialogIndex >= currentConversation.lines.Count)
        {
            endDialog();
        }
        else
        {
            currentLine = currentConversation.lines[dialogIndex];
            getLineLength();
            showDialog();
        }
    }

    private void timer()
    {
        dialogTimer -= Time.deltaTime;
        if (dialogTimer <= 0)
        {
            nextLine();
        }
    }

    private void endDialog()
    {
        showingDialog = false;
        dialogDisplay.color = new Color(0, 0, 0, 0);
        dialogTimer = 0;
        currentLine = null;
        currentConversation = null;
    }
}
