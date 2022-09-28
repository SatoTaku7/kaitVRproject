using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour,ICombo
{
    public int combo { get; private set; }
    public int score { get; private set; }

    IPerformanceManager performanceManager;
    IStateChanger stateChanger;
    ILevelState levelState;

    // Start is called before the first frame update
    void Start()
    {
        performanceManager = GetComponent<IPerformanceManager>();
        stateChanger = GetComponent<IStateChanger>();
        levelState = GetComponent<ILevelState>();
    }

    //�S�̂̃X�R�A���X�V
    public void UpdateAllScore()
    {
        levelState.ScoreUpdate(ScoreCal(combo));
    }

    //�ꖇ���Ƃ̃X�R�A
    public int ScoreCal(int combo)
    {
        return (int)(100 * (1 + 0.3 * combo));

    }

    /// <summary>
    /// combo�������鎞�̊֐�
    /// </summary>
    public void ContinuousCombo()
    {
        combo = Mathf.Clamp(combo + 1, 0, 10);
    }
    /// <summary>
    /// �R���{�����Z�b�g����֐�
    /// </summary>
    public void ResetCombo()
    {
        combo = 0;
    }
}
