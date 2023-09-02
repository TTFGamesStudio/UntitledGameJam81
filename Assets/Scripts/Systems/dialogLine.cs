using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "data/dialog", fileName = "dialog")]
public class dialogLine : ScriptableObject
{
    public string dialog;
    public AudioClip soundClip;
    public float length;
    public float pauseAfter;
}

