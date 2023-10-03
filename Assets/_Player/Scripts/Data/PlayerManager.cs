using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Presentation presentaion;

    [SerializeField] AudioSource source;
    [SerializeField] Image image;

    public bool ready = false;
    float timer = 0;

    private void Start()
    {
        StartCoroutine(Player());
        timer = 0;
    }

    private IEnumerator Player()
    {
        while (!ready)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        Debug.Log(TransformTime(timer));
        Debug.Log("Play");

        source.PlayOneShot(presentaion.slide[0].audio);
        image.sprite = presentaion.slide[0].image;
    }

    private string TransformTime(float time)
    {
        int min = Mathf.FloorToInt(time / 60);
        int seg = Mathf.FloorToInt(time % 60);
        int ms = Mathf.FloorToInt((time * 1000) % 1000);

        string timeFormat = string.Format("{0:D2}:{1:D2}.{2:D3}", min, seg, ms);

        return timeFormat;
    }
}
