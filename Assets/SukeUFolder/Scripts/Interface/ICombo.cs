public interface ICombo
{
    int combo { get; }
    /// <summary>
    /// コンボ連続した時の処理
    /// </summary>
    void ContinuousCombo();
    /// <summary>
    /// コンボ回数をリセットする処理
    /// </summary>
    void ResetCombo();
    void UpdateAllScore();
    int ScoreCal(int combo);

}
