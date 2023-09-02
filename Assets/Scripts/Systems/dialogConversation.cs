using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "data/conversation", fileName = "conversation")]
public class dialogConversation : ScriptableObject
{
    public List<dialogLine> lines;
    public bool alwaysShowSubtitles;
    public bool dontPlayAudio;
    public bool continueGameplay;
}
