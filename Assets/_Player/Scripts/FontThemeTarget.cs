using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SophiaPlayer
{
    public class FontThemeTarget : MonoBehaviour
    {
        public ThemeManager.TargetEnum color;
        private TMP_Text text;

        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<TMP_Text>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void setFont(TMP_FontAsset font, Color color)
        {
            text.font = font;
            text.color = color;
        }
    }
}