
public interface IPerformanceManager
{
    //�R���{��Ԃ̎��
    enum ComboState
    {
        ComboLow,
        ComboMid,
        ComboMax,
    }
    //���݂̃R���{���
    ComboState currentComboState { get; }
    //�R���{�񐔂����l�����������̏���
    void ComboLevelUp();
    //�R���{�񐔂����Z�b�g����鏈��
    void ComboReset();
    //�Q�[���I�[�o�[�ɂȂ����Ƃ��̏���
    void GameOver();
}
