using Doozy.Engine.Themes;
using Doozy.Engine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            s.GetComponent<Slide>().setData(sd);
            slides.Add(s);
        }

        static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(_R.Next(v.Length));
        }
    }
}