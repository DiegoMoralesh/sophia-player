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
        public List<double> segPhrase;
        public string[] keyPhrase;
        private int index = 0;

        public void setData(SlideData sd, List<double> segPhrase) {
            text.text = sd.content;
            picture.sprite = sd.image;
            audio.clip = sd.audio;
            this.segPhrase = segPhrase;
            keyPhrase = sd.keyPhrase;
            mainText.enabled = false;
        }

        private void Update()
        {
            if (audio.isPlaying)
            {
                float actualTime = audio.time;

                if (keyPhrase.Length > index)
                {
                    if (segPhrase[index]+1 <= actualTime)
                    {
                        FindObjectOfType<PlayerManager>().ShowKeyPhrase(mainText.transform, keyPhrase[index]);
                        index++;
                    }
                }
            }
        }

        private void OnDisable()
        {
            index = 0;
        }
    }
}