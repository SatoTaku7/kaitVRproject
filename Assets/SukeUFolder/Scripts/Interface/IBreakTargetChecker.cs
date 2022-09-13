public interface IBreakTargetChecker
{
    /// <summary>
    /// 的が割れた時の処理
    /// </summary>
    void BreakTarget();
    /// <summary>
    /// コンボ的が割れた時の処理
    /// </summary>
    /// <param name="combo">現在のコンボ数を引数に入力</param>
    void BreakTarget(int combo);

}