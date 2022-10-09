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
    /// ���_
    /// </summary>
    public int score { get; private set; }
    /// <summary>
    /// �ő�R���{��
    /// </summary>
    public int maxCombo { get; private set; }
    /// <summary>
    /// �^�C�g���̓I�������Ă���Q�[���I�[�o�[�܂ł̑����o�ߎ���
    /// </summary>
    public float elapsedTime { get; private set; }

    public int targetCount { get; private set; }
    public void SetRecord(int score, int maxCombo, float elapsedTime, int targetCount)
    {
        this.score = score;
        this.maxCombo = maxCombo;
        this.elapsedTime = elapsedTime;
        ScoreText.text = $"score:{score}";
        DetailText.text = $"�ő�R���{:{maxCombo}\n�ϋv����:{elapsedTime:f1}\n�I�j��:{ targetCount}\n�I�j�󕽋ώ���:{ elapsedTime/(float)targetCount:f2}";

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
