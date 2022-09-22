using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour,ICombo
{
    public int combo { get; private set; }
    public int score { get; private set; }
    public int allScore { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //全体のスコアを更新
    public void UpdateAllScore()
    {
        ScoreCal(combo);
        allScore += score;
    }

    //一枚ごとのスコア
    public int ScoreCal(int combo)
    {
        return (int)(100 * (1 + 0.3 * combo));
    }

    /// <summary>
    /// combo増加する時の関数
    /// </summary>
    public void ContinuousCombo()
    {
        combo = Mathf.Clamp(combo + 1, 0, 10);
    }
    /// <summary>
    /// コンボをリセットする関数
    /// </summary>
    public void ResetCombo()
    {
        combo = 0;
    }
}
