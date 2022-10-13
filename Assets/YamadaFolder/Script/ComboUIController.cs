using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUIController : MonoBehaviour
{
    private Image[] displayComboUI = new Image[11];
    private float[] amountY = new float[11];
    public float firstAmountY;
    public float minY = 1.5f;
    public float maxY = 0.5f;

    private Coroutine coroutine = null;

    //Debug—p
    public float comboNum = 0;
    public bool breakTarget = false;
    private bool once = true;
    public float plusNum = 0.5f;
    public float waitTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        displayComboUI = GetComponentsInChildren<Image>();
        for(int i = 0; i < displayComboUI.Length; i++)
        {
            displayComboUI[i].fillAmount = 0;
        }
        StartCoroutine(loop());
        StartCoroutine(loop2());
        StartCoroutine(loop3());
        StartCoroutine(loop4());
        StartCoroutine(loop5());
        StartCoroutine(loop6());
        StartCoroutine(loop7());
        StartCoroutine(loop8());
        StartCoroutine(loop9());
        StartCoroutine(loop10());
        StartCoroutine(loop11());
        StartCoroutine(loop12());
        StartCoroutine(loop13());
        StartCoroutine(loop14());
        //StartCoroutine(loop15());
    }

    // Update is called once per frame
    void Update()
    {
        if (breakTarget && once)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine("coolDown");
            once = false;
        }
        waitTime = 0.4f - (comboNum / 50f);
    }

    private IEnumerator coolDown()
    {
        for(int i = 0; i < 5; i++)
        {
            plusNum = 0.5f - (0.1f * i);
            yield return new WaitForSeconds(waitTime + 0.1f);
        }
        breakTarget = false;
        yield return new WaitForSeconds(waitTime);
        once = true;
    }

    private IEnumerator loop()
    {
        yield return new WaitForSeconds(0.14f);
        while (true)
        {
            if (breakTarget)
            {
                minY = 0.05f + (comboNum / 40f) + (plusNum * 0.5f);
                maxY = Mathf.Min(0.225f + (comboNum / 15f) + plusNum, 1f);
            }
            else
            {
                minY = 0.01f + (comboNum / 40f);
                maxY = 0.225f + (comboNum / 15f);
            }
            

            firstAmountY = Random.Range(minY, maxY);

            displayComboUI[0].fillAmount = firstAmountY;
            
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop2()
    {
        yield return new WaitForSeconds(0.13f);
        StartCoroutine(loop3());
        while (true)
        {
            displayComboUI[1].fillAmount = displayComboUI[0].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop3()
    {
        yield return new WaitForSeconds(0.12f);
        while (true)
        {
            displayComboUI[2].fillAmount = displayComboUI[1].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop4()
    {
        yield return new WaitForSeconds(0.11f);
        while (true)
        {
            displayComboUI[3].fillAmount = displayComboUI[2].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop5()
    {
        yield return new WaitForSeconds(0.10f);
        while (true)
        {
            displayComboUI[4].fillAmount = displayComboUI[3].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop6()
    {
        yield return new WaitForSeconds(0.09f);
        while (true)
        {
            displayComboUI[5].fillAmount = displayComboUI[4].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop7()
    {
        yield return new WaitForSeconds(0.08f);
        while (true)
        {
            displayComboUI[6].fillAmount = displayComboUI[5].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop8()
    {
        yield return new WaitForSeconds(0.07f);
        while (true)
        {
            displayComboUI[7].fillAmount = displayComboUI[6].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop9()
    {
        yield return new WaitForSeconds(0.06f);
        while (true)
        {
            displayComboUI[8].fillAmount = displayComboUI[7].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop10()
    {
        yield return new WaitForSeconds(0.05f);
        while (true)
        {
            displayComboUI[9].fillAmount = displayComboUI[8].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop11()
    {
        yield return new WaitForSeconds(0.04f);
        while (true)
        {
            displayComboUI[10].fillAmount = displayComboUI[9].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop12()
    {
        yield return new WaitForSeconds(0.03f);
        while (true)
        {
            displayComboUI[11].fillAmount = displayComboUI[10].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop13()
    {
        yield return new WaitForSeconds(0.02f);
        while (true)
        {
            displayComboUI[12].fillAmount = displayComboUI[11].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop14()
    {
        yield return new WaitForSeconds(0.01f);
        while (true)
        {
            displayComboUI[13].fillAmount = displayComboUI[12].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop15()
    {
        //yield return new WaitForSeconds(0.01f);
        while (true)
        {
            displayComboUI[10].fillAmount = displayComboUI[9].fillAmount;
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private IEnumerator loop20()
    {
        yield return new WaitForSeconds(waitTime / 2);
        while (true)
        {
            for (int i = 1; i < 6; i++)
            {
                displayComboUI[i * 2].fillAmount = displayComboUI[i * 2 - 1].fillAmount;
            }
            yield return new WaitForSeconds(waitTime / 2);

            for (int i = 1; i < 6; i++)
            {
                displayComboUI[i * 2 - 1].fillAmount = displayComboUI[i * 2 - 2].fillAmount;
            }
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    private void setPosY()
    {
        firstAmountY = Random.Range(minY, maxY);
        displayComboUI[0].fillAmount = firstAmountY;
        for (int i = 11; i < displayComboUI.Length; i--)
        {
            displayComboUI[i].fillAmount = displayComboUI[i - 1].fillAmount;
        }
    }
}
