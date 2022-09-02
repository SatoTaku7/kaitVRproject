public interface IStateChanger
{
    //ゲームの状態
    enum GameState
    {
        Title,
        Game,
        Result,

    }
    //現在の状態
    GameState currentState { get; }
    //ゲームを開始する処理
    void GameStart();
    //ゲームオーバーの処理
    void GameOver();
    //タイトルに行くときの処理
    void GoTitle();

}
