using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour, IResultManager
{
    int[] ranking = new int[4];
    public GameObject ResultUI;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI DetailText;
    [SerializeField] TextMeshProUGUI RankingText;
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

    private void Start()
    {

        ranking[0] = PlayerPrefs.GetInt("First", 0);
        ranking[1] = PlayerPrefs.GetInt("Second", 0);
        ranking[2] = PlayerPrefs.GetInt("Third", 0);
    }
    public void SetRecord(int score, int maxCombo, float elapsedTime, int targetCount)
    {
        this.score = score;
        this.maxCombo = maxCombo;
        this.elapsedTime = elapsedTime;
        SetRanking(score);
        ScoreText.text = $"score:{score}";
        DetailText.text = $"最大コンボ:{maxCombo}\n耐久時間:{elapsedTime:f1}\n的破壊数:{ targetCount}\n的破壊平均時間:{ elapsedTime/(float)targetCount:f2}";
        RankingText.text = $"1位:{ranking[0]}\n2位:{ranking[1]}\n3位:{ranking[2]}";
    }

    public void SetRanking(int score)
    {
        ranking[3] = score;
        for (int i = 0; i < ranking.Length; i++)
        {
            for (int j = i; j < ranking.Length; j++)
            {
                if (ranking[i] < ranking[j])
                {
                    int x = ranking[j];
                    ranking[j] = ranking[i];
                    ranking[i] = x;
                }
            }
        }

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
