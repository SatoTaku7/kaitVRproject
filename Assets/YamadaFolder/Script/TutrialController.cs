using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutrialController : MonoBehaviour
{
    public int currentDisplayNum = 0;//現在表示しているパネルの数字
    [SerializeField] GameObject[] explainObjs = new GameObject[5];//表示するパネル群

    [SerializeField] Image nextTimer;//パネル下部のバー
    private Coroutine coroutine;

    [SerializeField] GameObject pointImage;//パネル上部

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < explainObjs.Length; i++)
        {
            explainObjs[i].SetActive(false);
        }
        explainObjs[currentDisplayNum].SetActive(true);

        coroutine = StartCoroutine("fade");
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            goNext(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            goNext(true);
        }*/
    }

    private IEnumerator fade()
    {
        nextTimer.fillAmount = 0;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 100; i++)
        {
            nextTimer.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        goNext(true);
        yield break;
    }

    public void goNext(bool plus)
    {
        if(plus)
            currentDisplayNum++;
        else
            currentDisplayNum--;

        if (currentDisplayNum < 0)
            currentDisplayNum = explainObjs.Length - 1;
        if (currentDisplayNum >= explainObjs.Length)
            currentDisplayNum = 0;

        pointImage.transform.localPosition = new Vector3(-0.42f + (currentDisplayNum * 0.21f), 1.3f, 0);

        for (int i = 0; i < explainObjs.Length; i++)
        {
            explainObjs[i].SetActive(false);
        }
        explainObjs[currentDisplayNum].SetActive(true);

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine("fade");
    }
}
