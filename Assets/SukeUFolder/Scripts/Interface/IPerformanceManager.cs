
public interface IPerformanceManager
{
    //コンボ状態の種類
    enum ComboState
    {
        ComboLow,
        ComboMid,
        ComboMax,
    }
    //現在のコンボ状態
    ComboState currentComboState { get; }
    /// <summary>
    /// コンボ状態が変わるときのコールバック　OnChangeState.Invoke()で呼び出し
    /// </summary>
    event System.Action OnChangeState;
    void ChangeState(ComboState nextState);
    //コンボ回数に応じた処理
    void ComboPerformance(int combo);
    //ゲームオーバーになったときの処理
    void GameOver();
}
