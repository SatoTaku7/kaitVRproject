
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
    //コンボ回数が一定値を上回った時の処理
    void ComboLevelUp();
    //コンボ回数がリセットされる処理
    void ComboReset();
    //ゲームオーバーになったときの処理
    void GameOver();
}
