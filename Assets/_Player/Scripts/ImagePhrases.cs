using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImagePhrases : MonoBehaviour
{
    [HideInInspector]
    public List<string> url;
    [HideInInspector]
    public List<Sprite> image;

    public Image i; 

    public IEnumerator getImageFromUrl()
    {
        for (int i = 0; i < url.Count; i++)
        {
            WWW www = new WWW(url[0]);
            yield return www;

            if (www.error != null)
            {
                Debug.LogError("Error al cargar la imagen desde la URL: " + url[i]);
            }
            else
            {
                Texture2D loadedTexture = www.texture;

                // Crear un sprite a partir de la textura
                Sprite sprite = Sprite.Create(loadedTexture, new Rect(0, 0, loadedTexture.width, loadedTexture.height), Vector2.one * 0.5f);

                // Asignar el sprite al nombre correspondiente en la lista de nombres
                image.Add(sprite);
            }
        }

        i.sprite = image[0];
    }
    
}

