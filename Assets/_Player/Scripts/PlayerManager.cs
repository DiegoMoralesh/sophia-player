using Doozy.Engine.Themes;
using Doozy.Engine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace SophiaPlayer
{
    public class PlayerManager : MonoBehaviour
    {
        public List<GameObject> slides;
        int currentSlide = 0;
        public Button next, prev, play, stop;
        static System.Random _R = new System.Random();
        public UIView controls, loading;

        void Start()
        {
            next.onClick.AddListener(nextClick);
            prev.onClick.AddListener(prevClick);
            stop.onClick.AddListener(stopClick);
            play.onClick.AddListener(playClick);
        }

        public void fillData(List<SlideData> sd) {
            
            foreach (SlideData d in sd)
            {
                intaziateSlide(d);
            }
            loading.Hide();
            controls.Show();
            playClick();
        }

        public void nextClick()
        {
            if (currentSlide < slides.Count)
            {
                hideAllSlides();
                currentSlide++;
                slides[currentSlide].GetComponent<UIView>().Show();
            }
        }

        public void prevClick()
        {
            if (currentSlide > 0)
            {
                hideAllSlides();
                currentSlide--;
                slides[currentSlide].GetComponent<UIView>().Show();
            }
        }

        public void playClick()
        {
            slides[currentSlide].GetComponent<UIView>().Show();
        }

        public void stopClick()
        {
            hideAllSlides();
        }

        void hideAllSlides()
        {
            foreach (GameObject slide in slides)
            {
                slide.GetComponent<UIView>().InstantHide();
            }
        }

        void intaziateSlide(SlideData sd)
        {
            var value = RandomEnumValue<ThemeManager.LayoutEnum>();

            GameObject s = GameObject.Instantiate((UnityEngine.GameObject)Resources.Load("Layout/Style1/"+value.ToString()), Vector3.zero, Quaternion.identity, transform);
            s.GetComponent<Slide>().setData(sd, KeyPhrase(sd));
            slides.Add(s);
        }

        static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(_R.Next(v.Length));
        }

        List<double> KeyPhrase(SlideData sd)
        {
            List<double> segPhrase = new List<double>(); 
            for (int x = 0; x < sd.keyPhrase.Length; x++)
            {
                string[] phraseSplit = sd.keyPhrase[x].Split(' ');
                Similarity s = new Similarity();
                s.grade = 0;
                s.text = "";
                s.seg = 0;

                for (int i = 0; i < sd.srt.Length; i++)
                {  
                    if (sd.srt[i].text.Equals(phraseSplit[0], StringComparison.OrdinalIgnoreCase))
                    {
                        string aux = "";
                        for (int e = 0; e < phraseSplit.Length; e++)
                        {
                            aux += sd.srt[i + e].text+" ";
                        }

                        double similarity = CalculateSimilarity(aux, sd.keyPhrase[x]);
                        double similarityThreshold = 0.3;

                        if (similarity >= similarityThreshold)
                        {
                            if(s.grade < similarity)
                            {
                                s.grade = similarity;
                                s.text = sd.keyPhrase[x];
                                s.seg = sd.srt[i].start_time;
                            }
                        }
                    }
                }

                segPhrase.Add(s.seg);
            }
            return segPhrase;
        }

        public double CalculateSimilarity(string string1, string string2)
        {
            int string1Length = string1.Length;
            int string2Length = string2.Length;

            int[,] distanceMatrix = new int[string1Length + 1, string2Length + 1];

            for (int i = 0; i <= string1Length; i++)
            {
                distanceMatrix[i, 0] = i;
            }

            for (int j = 0; j <= string2Length; j++)
            {
                distanceMatrix[0, j] = j;
            }

            for (int i = 1; i <= string1Length; i++)
            {
                for (int j = 1; j <= string2Length; j++)
                {
                    int cost = (string1[i - 1] == string2[j - 1]) ? 0 : 1;
                    distanceMatrix[i, j] = Math.Min(
                        Math.Min(distanceMatrix[i - 1, j] + 1, distanceMatrix[i, j - 1] + 1),
                        distanceMatrix[i - 1, j - 1] + cost
                    );
                }
            }

            double distance = distanceMatrix[string1Length, string2Length];
            double maxLength = Math.Max(string1Length, string2Length);

            return 1.0 - (distance / maxLength);
        }

        [SerializeField] GameObject prefabKeyPhrase;
            
        public void ShowKeyPhrase(Transform content, string phrase)
        {
            TMP_Text TMPtext = GameObject.Instantiate(prefabKeyPhrase, content).GetComponent<TMP_Text>();
            TMPtext.text = phrase;
        }
    }
}

[System.Serializable]
public struct Similarity
{
    public double grade;
    public string text;
    public double seg;
}