using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class currentScoreText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    //���l���ς�鎞�ɏ��X�ɕς�邽�ߗp
    public Action OnComplete = null;//�I��������̃R�[���o�b�N

    private float speed;
    private float number;
    private float targetNumber;

    private Coroutine playCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "0";
    }

    public void Reset()
    {
        _text.text = "0";
        number = 0;
    }

    // ���̒l���� to_number �ɏ��X�Ɉڍs
    public void SlideToNumber(int to_number, float duration)
    {
        targetNumber = to_number;
        speed = ((targetNumber - number) / duration);

        if (playCoroutine != null)
        {
            StopCoroutine(playCoroutine);
        }
        playCoroutine = StartCoroutine("slideTo");
    }

    private IEnumerator slideTo()
    {
        while (true)
        {
            var delta = speed * Time.deltaTime;
            var next_number = number + delta;
            _text.text = ((int)next_number).ToString();

            number = next_number;

            if (UnityEngine.Mathf.Sign(speed) * (targetNumber - number) <= 0.0f)
            {
                break;
            }
            yield return null;
        }
        playCoroutine = null; ;
        number = targetNumber;
        _text.text = ((int)number).ToString();
        if (OnComplete != null)
        {
            OnComplete();
            OnComplete = null;
        }
    }
}
