public interface IStateChanger
{
    //�Q�[���̏��
    enum GameState
    {
        Title,
        Game,
        Result,

    }
    //���݂̏��
    GameState currentState { get; }
    //�Q�[�����J�n���鏈��
    void GameStart();
    //�Q�[���I�[�o�[�̏���
    void GameOver();
    //�^�C�g���ɍs���Ƃ��̏���
    void GoTitle();

}
