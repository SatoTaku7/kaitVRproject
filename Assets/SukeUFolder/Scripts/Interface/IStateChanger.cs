public interface IStateChanger
{
    /// <summary>
    /// ゲームの状態
    /// </summary>

    enum GameState
    {
        Title,
        Game,
        Result,
    }
    /// <summary>
    /// 現在の状態
    /// </summary>
    GameState currentState { get; }
    /// <summary>
    /// 状態が変わるときのコールバック　OnChangeState.Invoke()で呼び出し
    /// </summary>
    event System.Action OnChangeState;
    /// <summary>
    /// 状態を変化させる時に呼ぶ
    /// </summary>
    /// <param name="nextState">ここには列挙型の状態から選んで入力</param>
    void ChangeState(GameState nextState);
    /// <summary>
    ///ゲームを開始する処理
    /// </summary>
    void GameStart();
    /// <summary>
    /// ゲームオーバーの処理
    /// </summary>
    void GameOver();
    /// <summary>
    ///タイトルに行くときの処理
    /// </summary>
    void GoTitle();

}
