using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParagraphsManager : MonoBehaviour
{
    PlayerManagerPrueba player;
    public Paragraphs[] paragraphs;
    Sprite auxSprite;
    AudioClip auxAudio;
    public List<SlideData>  slides; 

    private void Start()
    {
        player = GetComponent<PlayerManagerPrueba>();
    }

    public IEnumerator fill()
    {
        //Presentation p = player.presentation;
        slides = new List<SlideData>();

        for (int i = 0; i < paragraphs.Length; i++)
        {
            SlideData slide = new SlideData();
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

            slides.Add(slide);
        }

        player.ready = true;
        FindObjectOfType<SophiaPlayer.PlayerManager>().fillData(slides);
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