using Doozy.Engine.Themes;
using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> slides;
    int currentSlide = 0;
    public Button next, prev, play, stop;
    void Start()
    {
        slides.Add(GameObject.Instantiate((UnityEngine.GameObject)Resources.Load("Layout/Style1/Title"), Vector3.zero, Quaternion.identity, transform));
        slides.Add(GameObject.Instantiate((UnityEngine.GameObject)Resources.Load("Layout/Style1/LeftThird"), Vector3.zero, Quaternion.identity, transform));
        slides.Add(GameObject.Instantiate((UnityEngine.GameObject)Resources.Load("Layout/Style1/RightThird"), Vector3.zero, Quaternion.identity, transform));

        slides[currentSlide].GetComponent<UIView>().Show();

        next.onClick.AddListener(nextClick);
        prev.onClick.AddListener(prevClick);
        stop.onClick.AddListener(stopClick);
        play.onClick.AddListener(playClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextClick()
    {
        if (currentSlide < slides.Count) {
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
        foreach(GameObject slide in slides)
        {
            slide.GetComponent<UIView>().InstantHide();
        }
    }
}