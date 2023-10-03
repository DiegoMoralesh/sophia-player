using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phrases : MonoBehaviour
{
    public List<string> phrases;
    public List<string[]> keyPhrases;
    public List<Srt[]> srt;

    private void Awake()
    {
        keyPhrases = new List<string[]>();
        srt = new List<Srt[]>();
    }
}
