using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour,ITimer
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsCounting { get; private set; }
    /// <summary>
    /// カウントダウンタイマー
    /// </summary>
    public float countDownTimer { get; private set; }
    /// <summary>
    /// カウントダウンの上限を設定する
    /// </summary>
    public void SetTimer()
    {

    }
    /// <summary>
    /// カウントダウンをリセットする
    /// </summary>
    public void ResetTimer(float ResetCount)
    {
        countDownTimer = ResetCount;
    }
    /// <summary>
    /// カウントダウンを始める
    /// </summary>
    public void StartTimer()
    {

    }
    /// <summary>
    /// カウントダウンを止める
    /// </summary>
    public void StopTimer()
    {

    }
}
