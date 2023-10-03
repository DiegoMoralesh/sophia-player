using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioPhrases : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [HideInInspector]
    public List<string> url;
    [HideInInspector]
    public List<AudioClip> audioClip;

    public IEnumerator getAudioFromUrl()
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
                audioClip.Add(www.GetAudioClip(false, false));
            }
        }

        source.PlayOneShot(audioClip[0]);
    }

    private void OnApplicationQuit()
    {
        audioClip = new List<AudioClip>();
    }
}

