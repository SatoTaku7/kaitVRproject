public interface ITimer
{
    bool IsCounting { get; }
    /// <summary>
    /// �J�E���g�_�E���^�C�}�[
    /// </summary>
    float countDownTimer { get; }
    /// <summary>
    /// �J�E���g�_�E���̏����ݒ肷��
    /// </summary>
    void SetTimer();
    /// <summary>
    /// �J�E���g�_�E�������Z�b�g����
    /// </summary>
    void ResetTimer(float ResetCount);
    /// <summary>
    /// �J�E���g�_�E�����n�߂�
    /// </summary>
    void StartTimer();
    /// <summary>
    /// �J�E���g�_�E�����~�߂�
    /// </summary>
    void StopTimer();

}