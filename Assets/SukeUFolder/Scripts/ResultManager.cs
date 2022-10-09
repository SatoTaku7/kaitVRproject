using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour, IResultManager
{
    public GameObject ResultUI;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI DetailText;
    /// <summary>
    /// 得点
    /// </summary>
    public int score { get; private set; }
    /// <summary>
    /// 最大コンボ数
    /// </summary>
    public int maxCombo { get; private set; }
    /// <summary>
    /// タイトルの的を撃ってからゲームオーバーまでの総合経過時間
    /// </summary>
    public float elapsedTime { get; private set; }

    public int targetCount { get; private set; }
    public void SetRecord(int score, int maxCombo, float elapsedTime, int targetCount)
    {
        this.score = score;
        this.maxCombo = maxCombo;
        this.elapsedTime = elapsedTime;
        ScoreText.text = $"score:{score}";
        DetailText.text = $"最大コンボ:{maxCombo}\n耐久時間:{elapsedTime:f1}\n的破壊数:{ targetCount}\n的破壊平均時間:{ elapsedTime/(float)targetCount:f2}";

    }

    public void EnableUI()
    {
        ResultUI.SetActive(true);
    }
    public void DisableUI()
    {
        ResultUI.SetActive(false);

    }
}
