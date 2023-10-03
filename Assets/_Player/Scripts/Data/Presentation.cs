using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Presentation
{
    public List<Slide> slide;
}

[System.Serializable]
public class Slide
{
    public string content;
    public string[] keyPhrase;
    public Srt[] srt;
    public Sprite image;
    public AudioClip audio;
    public string style;
    public string layout;
    public string color;
}
