using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIFader : MonoBehaviour
{
    List<Image> images = new List<Image>();
    List<TextMeshProUGUI> textMeshes = new List<TextMeshProUGUI>();
    List<Renderer> materials = new List<Renderer>();
    float alpha=1f;
    Coroutine coroutine;
    Vector3 defaultPosition;
    private void Start()
    {
        defaultPosition = transform.position;
        RecursiveSetActive(this.gameObject);
        Invisible();
    }

    public void AppearUI()
    {
       coroutine= StartCoroutine(FadeOut());
        SoundManager.Instance.PlaySeByName("Result_display2", gameObject);
    }
    public void DisappearUI()
    {
        transform.position = defaultPosition;
        Invisible();
        if(coroutine!=null)
        StopCoroutine(coroutine);
    }

    private void RecursiveSetActive(GameObject a_CheckObject)
    {
        // 対象オブジェクトの子オブジェクトをチェックする
        foreach (Transform child in a_CheckObject.transform)
        {
            GameObject childObject = child.gameObject;
            if (childObject.GetComponent<Image>() != null)
            {
                images.Add(childObject.GetComponent<Image>());
            }else if(childObject.GetComponent<TextMeshProUGUI>() != null)
            {
                textMeshes.Add(childObject.GetComponent<TextMeshProUGUI>());
            }
            else if (childObject.GetComponent<Renderer>() != null)
            {
                materials.Add(childObject.GetComponent<Renderer>());
            }

            // 再帰的に全ての子オブジェクトを処理する
            RecursiveSetActive(childObject);
        }
    }
    private void Invisible()
    {
        alpha = 0f;
        foreach (var image in images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }
        foreach (var textMesh in textMeshes)
        {
            textMesh.alpha = alpha;
        }
        foreach (var material in materials)
        {
            material.material.color = new Color(material.material.color.r, material.material.color.g, material.material.color.b, alpha);
        }
    }
    IEnumerator FadeIn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.025f);
            alpha -= 0.02f;
            foreach(var image in images)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            }
            foreach (var textMesh in textMeshes)
            {
                textMesh.alpha = alpha;
            }
             foreach(var material in materials)
            {
                material.material.color = new Color(material.material.color.r, material.material.color.g, material.material.color.b, alpha);
            }
            
            if (alpha <= 0f) // 不透明度が0以下のとき
            {
                alpha = 0f;
                break; // 繰り返し終了
            }
        }
    }
    IEnumerator FadeOut()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.025f); 
            alpha += 0.02f;
            transform.Translate(0, alpha*0.05f,0);
            foreach (var image in images)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            }
            foreach (var textMesh in textMeshes)
            {
                textMesh.alpha = alpha;
            }
            foreach (var material in materials)
            {
                material.material.color = new Color(material.material.color.r, material.material.color.g, material.material.color.b, alpha);
            }
            if (alpha >= 1f) // 不透明度が0以下のとき
            {
                alpha = 1f;
                break; // 繰り返し終了
            }
        }
    }
}
