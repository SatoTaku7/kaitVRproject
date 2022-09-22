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

    //�S�̂̃X�R�A���X�V
    public void UpdateAllScore()
    {
        ScoreCal(combo);
        allScore += score;
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
