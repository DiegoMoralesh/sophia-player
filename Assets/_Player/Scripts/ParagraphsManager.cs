using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParagraphsManager : MonoBehaviour
{
    public Paragraphs[] paragraphs;
    Phrases phrases;
    AudioPhrases audioPhrases;
    ImagePhrases imagePhrases;
    private void Start()
    {
        phrases = GetComponent<Phrases>();
        audioPhrases = GetComponent<AudioPhrases>();
        imagePhrases = GetComponent<ImagePhrases>();
    }

    public void fill()
    {
        //LLena las frases, palabras claves y srt
        phrases.phrases.AddRange(paragraphs.Select(p => p.content));
        phrases.keyPhrases.AddRange(paragraphs.Select(p => p.keyPhrases));
        phrases.srt.AddRange(paragraphs.Select(p => p.srt));

        //Llena los audios
        audioPhrases.url.AddRange(paragraphs.Select(p => p.audioUrl));
        StartCoroutine(audioPhrases.getAudioFromUrl());

        //Llena las imagenes
        imagePhrases.url.AddRange(paragraphs.Select(p => p.imageData.finalImage.url));
        StartCoroutine(imagePhrases.getImageFromUrl());
    }
}
