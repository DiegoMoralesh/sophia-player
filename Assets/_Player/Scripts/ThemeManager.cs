using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SophiaPlayer
{
    public class ThemeManager : MonoBehaviour
    {
        [Dropdown("themes", "themeName")]
        public ColorTheme selectedTheme;
        private ColorTheme currentTheme;
        [Dropdown("fonts")]
        public TMP_FontAsset selectedFont;
        private TMP_FontAsset currentFont;
        public List<TMP_FontAsset> fonts;
        public List<ColorTheme> themes;
        public enum TargetEnum { primary, secondary, text, background }
        public enum LayoutEnum { Title, Leftthird, RightThird, LeftMiddle, RightMiddle, Middle, Center };
        public enum StylesEnum { style1 };
        public FontThemeTarget[] fontTargets;
        public ColorThemeTarget[] colorTargets;
        public GameObject slideManager;

        // Start is called before the first frame update
        void Start()
        {
            currentFont = selectedFont;
            currentTheme = selectedTheme;
            slideManager = GameObject.Find("SlideManager");
        }

        // Update is called once per frame
        void Update()
        {
            foreach (ColorTheme theme in themes)
            {
                theme.text.a = 1;
                theme.background.a = 1;
                theme.primary.a = 1;
                theme.secondary.a = 1;
            }
            if (currentTheme != selectedTheme)
            {
                colorTargets = slideManager.GetComponentsInChildren<ColorThemeTarget>();
                foreach (ColorThemeTarget target in colorTargets)
                {
                    switch (target.color)
                    {
                        case TargetEnum.primary:
                            target.setColor(selectedTheme.primary);
                            break;
                        case TargetEnum.secondary:
                            target.setColor(selectedTheme.secondary);
                            break;
                        case TargetEnum.text:
                            target.setColor(selectedTheme.text);
                            break;
                        case TargetEnum.background:
                            target.setColor(selectedTheme.background);
                            break;
                        default:
                            break;
                    }
                }
                fontTargets = slideManager.GetComponentsInChildren<FontThemeTarget>();
                foreach (FontThemeTarget target in fontTargets)
                {
                    switch (target.color)
                    {
                        case TargetEnum.primary:
                            target.setFont(selectedFont, selectedTheme.primary);
                            break;
                        case TargetEnum.secondary:
                            target.setFont(selectedFont, selectedTheme.secondary);
                            break;
                        case TargetEnum.text:
                            target.setFont(selectedFont, selectedTheme.text);
                            break;
                        case TargetEnum.background:
                            target.setFont(selectedFont, selectedTheme.background);
                            break;
                        default:
                            break;
                    }

                }
                currentTheme = selectedTheme;
            }

            if (currentFont != selectedFont)
            {
                fontTargets = slideManager.GetComponentsInChildren<FontThemeTarget>();
                foreach (FontThemeTarget target in fontTargets)
                {
                    switch (target.color)
                    {
                        case TargetEnum.primary:
                            target.setFont(selectedFont, selectedTheme.primary);
                            break;
                        case TargetEnum.secondary:
                            target.setFont(selectedFont, selectedTheme.secondary);
                            break;
                        case TargetEnum.text:
                            target.setFont(selectedFont, selectedTheme.text);
                            break;
                        case TargetEnum.background:
                            target.setFont(selectedFont, selectedTheme.background);
                            break;
                        default:
                            break;
                    }

                }
                currentFont = selectedFont;
            }
        }
    }
}