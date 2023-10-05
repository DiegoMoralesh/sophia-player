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
    public List<SlideData>  slides; 

    private void Start()
    {
        player = GetComponent<PlayerManager>();
    }

    public IEnumerator fill()
    {
        //Presentation p = player.presentation;
        slides = new List<SlideData>();

        for (int i = 0; i < paragraphs.Length; i++)
        {
            SlideData slideData = new SlideData();
            slideData.content = paragraphs[i].content;
            slideData.keyPhrase = paragraphs[i].keyPhrases;
            slideData.srt = paragraphs[i].srt;
            yield return StartCoroutine(getImageFromUrl(paragraphs[i].imageData.finalImage.url));
            slideData.image = auxSprite;
            yield return StartCoroutine(getAudioFromUrl(paragraphs[i].audioUrl));
            slideData.audio = auxAudio;

            slideData.style = "";
            slideData.layout = "";
            slideData.color = "";

            slides.Add(slideData);
        }

        player.ready = true;
        GameObject.Find("SlideManager").GetComponent<SophiaPlayer.PlayerManager>().fillData(slides);
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