using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SophiaPlayer
{
    public class Slide : MonoBehaviour
    {
        // Start is called before the first frame update
        public TMP_Text text;
        public TMP_Text mainText;
        public AudioSource audio;
        public Image picture;

        public void setData(SlideData sd) {
            text.text = sd.content;
            picture.sprite = sd.image;
            audio.clip = sd.audio;
        }
    }
}