using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour,ITimer
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsCounting { get; private set; }
    /// <summary>
    /// �J�E���g�_�E���^�C�}�[
    /// </summary>
    public float countDownTimer { get; private set; }
    /// <summary>
    /// �J�E���g�_�E���̏����ݒ肷��
    /// </summary>
    public void SetTimer()
    {

    }
    /// <summary>
    /// �J�E���g�_�E�������Z�b�g����
    /// </summary>
    public void ResetTimer(float ResetCount)
    {
        countDownTimer = ResetCount;
    }
    /// <summary>
    /// �J�E���g�_�E�����n�߂�
    /// </summary>
    public void StartTimer()
    {

    }
    /// <summary>
    /// �J�E���g�_�E�����~�߂�
    /// </summary>
    public void StopTimer()
    {

    }
}
