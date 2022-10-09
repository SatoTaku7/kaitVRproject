public interface IResultManager
{
    /// <summary>
    /// 得点
    /// </summary>

    int score { get; }
    /// <summary>
    /// 最大コンボ数
    /// </summary>
    int maxCombo { get; }
    /// <summary>
    /// タイトルの的を撃ってからゲームオーバーまでの総合経過時間
    /// </summary>
    float elapsedTime { get; }
    int targetCount { get; }
    /// <summary>
    /// リザルトに受け渡す関数
    /// </summary>
    /// <param name="score">ゲーム中のスコア</param>
    /// <param name="maxCombo">最大コンボ数</param>
    /// <param name="elapsedTime">タイトルの的を撃ってからゲームオーバーまでの総合経過時間</param>
    void SetRecord(int score,int maxCombo,float elapsedTime,int targetCount);

    void EnableUI();
    void DisableUI();
}