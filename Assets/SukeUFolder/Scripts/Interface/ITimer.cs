public interface ITimer
{
    bool IsCounting { get; }
    /// <summary>
    /// カウントダウンタイマー
    /// </summary>
    float countDownTimer { get; }
    /// <summary>
    /// カウントダウンの上限を設定する
    /// </summary>
    void SetTimer();
    /// <summary>
    /// カウントダウンをリセットする
    /// </summary>
    void ResetTimer(float ResetCount);
    /// <summary>
    /// カウントダウンを始める
    /// </summary>
    void StartTimer();
    /// <summary>
    /// カウントダウンを止める
    /// </summary>
    void StopTimer();

}