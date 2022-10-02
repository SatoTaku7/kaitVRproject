
public interface IGunManager
{
    //リロードする時に呼び出される
    void Reload();
    //お助けマトを撃った時の処理
    void PowerUp();

    void PowerDown();
    //ゲームオーバーになったときの処理
    void GameOver();
}
