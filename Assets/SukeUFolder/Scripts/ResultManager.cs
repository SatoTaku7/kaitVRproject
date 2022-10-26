using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour, IResultManager
{
    int[] ranking = new int[4];
    string yourRank;
    public GameObject ResultUI;
    TextMeshProUGUI ScoreText;
    TextMeshProUGUI DetailText;
    TextMeshProUGUI RankingText;
    TextMeshProUGUI YourRankText;
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
        ScoreText=GameObject.FindGameObjectWithTag("ResultUI").transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        DetailText = GameObject.FindGameObjectWithTag("ResultUI").transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        RankingText= GameObject.FindGameObjectWithTag("ResultUI").transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        YourRankText= GameObject.FindGameObjectWithTag("ResultUI").transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        ranking[0] = PlayerPrefs.GetInt("First", 48700);
        ranking[1] = PlayerPrefs.GetInt("Second", 0);
        ranking[2] = PlayerPrefs.GetInt("Third", 0);
    }
    public void SetRecord(int score, int maxCombo, float elapsedTime, int targetCount)
    {
        this.score = score;
        this.maxCombo = maxCombo;
        this.elapsedTime = elapsedTime;
        var breakAverage = 0f;
        if (elapsedTime / (float)targetCount >= 0.01f)
        {
            breakAverage = elapsedTime / (float)targetCount;
        }
        else
        {
            breakAverage = 0f;
        }
        
        SetRanking(score);
        ScoreText.text = $"score:{score}";
        DetailText.text = $"最大コンボ:{maxCombo}\n耐久時間:{elapsedTime:f1}\n的破壊数:{ targetCount}\n的破壊平均時間:{ breakAverage:f2}";
        RankingText.text = $"1位:{ranking[0]}\n2位:{ranking[1]}\n3位:{ranking[2]}";
        YourRankText.text = $"あなたの順位{yourRank}";
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
        PlayerPrefs.SetInt("First", ranking[0]);
        PlayerPrefs.SetInt("Second", ranking[1]);
        PlayerPrefs.SetInt("Third", ranking[2]);
        for (int i = 0; i < ranking.Length-1; i++)
        {

            if (ranking[i] == score)
            {
                yourRank =$"{ i + 1}位";
                break;
            }
            else { yourRank = "ランク外"; }
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
