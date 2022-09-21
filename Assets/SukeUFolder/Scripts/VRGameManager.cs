
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGameManager : MonoBehaviour, ICombo, IStateChanger, IBreakTargetChecker
{
    //銃のマネージャーを参照して取得する必要がある。
    IGunManager gunManager;
    IResultManager resultManager;
    private int score = 0;
    public int combo { get; private set; } = 0;
    public IStateChanger.GameState currentState { get; private set; }
    public event System.Action OnChangeState;
    /// <summary>
    /// ステータスを変更するときに呼び出す
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeState(IStateChanger.GameState nextState)
    {
        currentState = nextState;
        if(OnChangeState!=null) OnChangeState.Invoke();
    }
    void Start()
    {
        OnChangeState += DebugTest;
        ChangeState(IStateChanger.GameState.Title);
    }

    public void DebugTest()
    {
        Debug.Log($"現在の状態:{currentState}");
    }
    // Update is called once per frame
    void Update()
    {
        if (currentState == IStateChanger.GameState.Title)
        {

        }else if(currentState == IStateChanger.GameState.Game){

        }else if(currentState == IStateChanger.GameState.Result)
        {

        }
    }
    /// <summary>
    /// combo増加する時の関数
    /// </summary>
    public void ContinuousCombo()
    {
        combo = Mathf.Clamp(combo + 1, 0, 10);
    }
    /// <summary>
    /// コンボをリセットする関数
    /// </summary>
    public void ResetCombo()
    {
        combo = 0;
    }
    /// <summary>
    /// コンボじゃない場合の処理
    /// </summary>
    public void BreakTarget()
    {
        //リロードを呼び出す
        gunManager.Reload();
    }
    /// <summary>
    /// コンボがあるときの処理
    /// </summary>
    /// <param name="comb"></param>
    public void BreakTarget(int comb)
    {
        //リロードを呼び出す
        gunManager.Reload();
    }
    

    //ここは最悪不必要になりそう
    private int ScoreCal(int combo)
    {
        return (int)(100 * (1 + 0.3 * combo));
    }


    public void GameStart()
    {
        combo = 0;
        score = 0;
        ChangeState(IStateChanger.GameState.Game);
        //ゲーム開始処理
    }
    public void GameOver()
    {
        //ゲームオーバーになったときの処理
        ChangeState(IStateChanger.GameState.Result);
        //resultManager.SetRecord(0,0,0);//リザルトに受け渡す
    }


    public void GoTitle()
    {
        //タイトルへ移動する処理
    }
}

