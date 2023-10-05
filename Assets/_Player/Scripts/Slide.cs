using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SophiaPlayer
{
    public class Slide : MonoBehaviour
    {
        public TMP_Text text;
        public TMP_Text mainText;
        public AudioSource audio;
        public Image picture;
        private List<Dictionary<int, string>> keyPhrase = new List<Dictionary<int, string>>();

        public void setData(SlideData sd) {
            text.text = sd.content;
            picture.sprite = sd.image;
            audio.clip = sd.audio;
            //keyPhrase = sd.keyPhrase;
        }

        private void Update()
        {
            if (audio.isPlaying)
            {
                float tiempoActual = audio.time;
            }
        }
    }
}