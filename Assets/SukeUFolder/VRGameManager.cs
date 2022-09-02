
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGameManager : MonoBehaviour,IScoreManager,IStateChanger
{
    int score=0;
    int combo = 0;
    public IStateChanger.GameState currentState { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BreakTarget()
    {
        //的が割れた時の処理
        combo = Mathf.Clamp(combo+1, 0, 10);
        score += ScoreCal(combo);
    }

    private int ScoreCal(int combo)
    {
        return (int)(100 * (1 + 0.3 * combo));
    }


    public void GameStart()
    {
        combo = 0;
        score = 0;
        //ゲーム開始処理
    }
    public void GameOver()
    {
        //ゲームオーバーになったときの処理
    }
    public void GoTitle()
    {
        //タイトルへ移動する処理
    }
}

