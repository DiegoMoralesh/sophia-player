using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorThemeTarget : MonoBehaviour
{
    public SlideThemeManager.TargetEnum color;
    private Image targetImg;
    

    // Start is called before the first frame update
    void Start()
    {
        targetImg = GetComponent<Image>();
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void setColor(Color color) { 
        targetImg.color = color;
    }
}
