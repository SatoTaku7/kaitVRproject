public interface ITimer
{
    public bool IsPlaying { get; }
    public float playTime { get; }
    float startTime { get; }
    float endTime { get; }
    /// <summary>
    /// カウントしているかどうか
    /// </summary>
    bool IsCounting { get; }
    /// <summary>
    /// カウントダウンタイマー
    /// </summary>
    float timer { get; }
    /// <summary>
    /// カウントダウンの上限を設定する
    /// </summary>
    void SetTimer(float upperlimitTime);
    /// <summary>
    /// カウントダウンをリセットする
    /// </summary>
    void ResetTimer();
    /// <summary>
    /// カウントダウンを始める
    /// </summary>
    void StartTimer();
    /// <summary>
    /// カウントダウンを止める
    /// </summary>
    void StopTimer();

    /// <summary>
    /// タイマーの開始処理
    /// </summary>
    void StartPlay();
    /// <summary>
    /// タイマーの停止処理
    /// </summary>
    void StopPlay();
    /// <summary>
    ///  ゲームの経過時間を初期化する
    /// </summary>
    void ResetPlayTime();

}