using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParagraphsManager : MonoBehaviour
{
    PlayerManager player;
    public Paragraphs[] paragraphs;
    Sprite auxSprite;
    AudioClip auxAudio;

    private void Start()
    {
        player = GetComponent<PlayerManager>();
    }

    public IEnumerator fill()
    {
        Presentation p = player.presentaion;
        p.slide = new List<Slide>();

        for (int i = 0; i < paragraphs.Length; i++)
        {
            Slide slide = new Slide();
            StartCoroutine(getImageFromUrl(paragraphs[i].imageData.finalImage.url));
            yield return StartCoroutine(getAudioFromUrl(paragraphs[i].audioUrl));
            slide.content = paragraphs[i].content;
            slide.keyPhrase = paragraphs[i].keyPhrases;
            slide.srt = paragraphs[i].srt;
            slide.image = auxSprite;
            slide.audio = auxAudio;
            slide.style = "";
            slide.layout = "";
            slide.color = "";

            p.slide.Add(slide);
        }

        player.ready = true;
    }

    public IEnumerator getImageFromUrl(string url)
    {

        WWW www = new WWW(url);
        yield return www;

        if (www.error != null)
        {
            Debug.LogError("Error al cargar la imagen desde la URL: " + url);
        }
        else
        {
            Texture2D loadedTexture = www.texture;

            // Crear un sprite a partir de la textura
            Sprite sprite = Sprite.Create(loadedTexture, new Rect(0, 0, loadedTexture.width, loadedTexture.height), Vector2.one * 0.5f);

            // Asignar el sprite al nombre correspondiente en la lista de nombres
            auxSprite = sprite;
        }
    }

    public IEnumerator getAudioFromUrl(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError("Error al cargar la imagen desde la URL: " + url);
        }
        else
        {
            auxAudio = www.GetAudioClip(false, false);
        }
    }
}