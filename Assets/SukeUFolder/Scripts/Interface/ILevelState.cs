public interface ILevelState
{
    /// <summary>
    /// 最大スコア
    /// </summary>
    int scoreSum { get;}
    /// <summary>
    /// 現在のレベル
    /// </summary>
    int currentLevel { get; }
    /// <summary>
    /// 最大スコアのアップデート
    /// </summary>
    /// <param name="addScore"></param>
    void ScoreUpdate(int addScore);

    void ChangeLevel(int num);

}
