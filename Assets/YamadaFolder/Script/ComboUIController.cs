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
        while (true)
        {
            if (breakTarget)
            {
                minY = 0.1f + (comboNum / 30f) + (plusNum * 0.75f);
                maxY = 0.225f + (comboNum / 15f) + plusNum;
            }
            else
            {
                minY = 0.1f + (comboNum / 30f);
                maxY = 0.225f + (comboNum / 15f);
            }

            if (maxY > 1)
                maxY = 1;

            firstAmountY = Random.Range(minY, maxY);

            displayComboUI[0].fillAmount = firstAmountY;
            for (int i = 1; i < 6; i++)
            {
                displayComboUI[i * 2].fillAmount = displayComboUI[i * 2 - 1].fillAmount;
            }
            yield return new WaitForSeconds(waitTime);

            for (int i = 1; i < 6; i++)
            {
                displayComboUI[i * 2 - 1].fillAmount = displayComboUI[i * 2 - 2].fillAmount;
            }
            yield return new WaitForSeconds(waitTime);
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
