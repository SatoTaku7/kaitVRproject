public interface IBreakTargetChecker
{
    /// <summary>
    /// 的が割れた時の処理
    /// </summary>
    void BreakTarget();
    /// <summary>
    /// お助け的が割れた時の処理
    /// </summary>
    void BreakAssistTarget();
}