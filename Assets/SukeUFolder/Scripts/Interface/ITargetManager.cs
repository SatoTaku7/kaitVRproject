
public interface ITargetManager
{
    /// <summary>
    /// 的を生み出す関数
    /// </summary>
    void TargetInit();

    /// <summary>
    /// 壊れた的の数
    /// </summary>
    int breakTargetCount { get; }
    /// <summary>
    /// 全ての的を消す処理
    /// </summary>
    void AllTargetDestroy();
    void Reset();
}

