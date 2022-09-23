using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour,ITimer
{
    IStateChanger stateChanger;
    public bool IsPlaying { get; private set; }
    public float playTime { get; private set; }
    Text text;
    private float currentStartTime;
    public float startTime { get; private set; } = 10f;
    public float endTime { get; private set; } = 0f;
    public bool IsCounting { get; private set; }
    /// <summary>
    /// カウントダウンタイマー
    /// </summary>
    public float timer { get; private set; }
    /// <summary>
    /// カウントダウンの上限を設定する
    /// </summary>
    public void SetTimer(float upperlimitTime)
    {
        startTime = upperlimitTime;
    }
    /// <summary>
    /// カウントダウンをリセットする
    /// </summary>
    public void ResetTimer()
    {
        currentStartTime = startTime;
        timer = startTime;
    }
    /// <summary>
    /// カウントダウンを始める
    /// </summary>
    public void StartTimer()
    {
        if (!IsCounting)
        {
            IsCounting = true;
        }
    }
    /// <summary>
    /// カウントダウンを止める
    /// </summary>
    public void StopTimer()
    {
        if (IsCounting)
        {
            IsCounting = false;

        }
    }

    /// <summary>
    /// ゲームの経過時間を計測する
    /// </summary>
    public void StartPlay()
    {
        IsPlaying = true;
        SetTimer(10f);
        ResetTimer();
        StartTimer();
    }
    /// <summary>
    /// ゲームの経過時間を停止する
    /// </summary>
    public void StopPlay()
    {
        IsPlaying = false;
        StopTimer();
    }
    /// <summary>
    ///  ゲームの経過時間を初期化する
    /// </summary>
    public void ResetPlayTime()
    {
        playTime = 0f;
    }


    void Start()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        text = transform.GetChild(0).GetComponentInChildren<Text>();
        stateChanger= GameObject.FindGameObjectWithTag("GameController").GetComponent<IStateChanger>();
    }
    private void Update()
    {
        if (IsPlaying)
        {
            playTime += Time.deltaTime;
            if (IsCounting)
            {
                timer = Mathf.Clamp(timer - Time.deltaTime, endTime, currentStartTime);
                text.text = $"{timer:f1}";
                if (timer == 0)
                {
                    stateChanger.ChangeState(IStateChanger.GameState.Result);
                }
            }

        }
    }
}
