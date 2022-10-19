using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialUI : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialimage;
    private float count;
    private int num;
    void Start()
    {
        count = 0;
        for(num = 0; num < 3; num++)
        {
            tutorialimage[num].GetComponent<CanvasGroup>().alpha = 0;
        }
        num = 0;
    }

    void Update()
    {
        ImageFade();
    }
    void ImageFade()
    {
        count += Time.deltaTime;
        if (count < 2)
        {
            if (tutorialimage[num].GetComponent<CanvasGroup>().alpha < 1)
            {
                tutorialimage[num].GetComponent<CanvasGroup>().alpha += 0.01f;
            }
        }
        if (count > 3)
        {
            if (tutorialimage[num].GetComponent<CanvasGroup>().alpha >= 0)
            {
                tutorialimage[num].GetComponent<CanvasGroup>().alpha -= 0.01f;
            }
            if (tutorialimage[num].GetComponent<CanvasGroup>().alpha <= 0)
            {
                tutorialimage[num].GetComponent<CanvasGroup>().alpha = 0;
                if (num == 2) num = 0;
                else num++;
                count = 0;
            }
        }
    }
}
