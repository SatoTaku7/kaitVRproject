
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGameManager : MonoBehaviour, IStateChanger, ILevelState
{
    #region Interface
    IGunManager gunManager;
    IResultManager resultManager;
    IPerformanceManager performanceManager;
    ITimer timer;
    ICombo comboManager;
    ITargetManager targetManager;
    #endregion  
    public int scoreSum { get; private set; }
    public int maxCombo { get; private set; }
    public float countDownTimer { get; private set; }
    public IStateChanger.GameState currentState { get; private set; }
    public int currentLevel { get; private set; }
    public event System.Action OnChangeState;
    /// <summary>
    /// ステータスを変更するときに呼び出す
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeState(IStateChanger.GameState nextState)
    {
        currentState = nextState;
       // if (OnChangeState != null) OnChangeState.Invoke();
       if(nextState== IStateChanger.GameState.Game)
        {
            //ゲーム開始時の初期化処理はここに書く
            targetManager.TargetInit();
            StartCoroutine("StartInterval");
        }else if(nextState == IStateChanger.GameState.Result)
        {
            //ゲーム終了時の処理はここに書く
            targetManager.AllTargetDestroy();
            timer.StopPlay();
        }else if (nextState == IStateChanger.GameState.Title)
        {
            //タイトルに戻った時の処理はここに書く
        }

    }
    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<ITimer>();
        if (GetComponent<IResultManager>() != null)
        {
            resultManager = GetComponent<IResultManager>();
        }
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<IGunManager>();
        targetManager = GetComponent<ITargetManager>();
        performanceManager = GetComponent<IPerformanceManager>();
        ChangeState(IStateChanger.GameState.Title);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == IStateChanger.GameState.Title)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Debug.Log("ゲーム始めます。");
                ChangeState(IStateChanger.GameState.Game);
            }
        }
        else if (currentState == IStateChanger.GameState.Game)
        {
            Debug.Log(scoreSum);
            if (scoreSum >= 22800)
            {
                ChangeLevel(7);
            }
            else if(scoreSum >= 16800)
            {
                ChangeLevel(6);
            }
            else if(scoreSum >= 11600)
            {
                ChangeLevel(5);
            }
            else if (scoreSum >= 8800)
            {
                ChangeLevel(4);
            }
            else if(scoreSum >= 4000)
            {
                ChangeLevel(3);
            }
            else if(scoreSum >= 1450)
            {
                ChangeLevel(2);
            }
            else if (scoreSum >= 700)
            {
                ChangeLevel(1);
            }
            else
            {
                ChangeLevel(0);
            }

        }
        else if (currentState == IStateChanger.GameState.Result)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Debug.Log("タイトル戻ります。");
                resultManager?.SetRecord(scoreSum, maxCombo, (int)timer.playTime);
                ChangeState(IStateChanger.GameState.Title);
            }
        }
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

    public void GameStart()
    {
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
    IEnumerator StartInterval()
    {
        yield return new WaitForSeconds(3f);
        timer.StartPlay();

    }
    void MaxCombo(int combo)
    {
        if (maxCombo < combo) maxCombo = combo;
    }

    public void ScoreUpdate(int addScore)
    {
        scoreSum += addScore;
        timer.ResetTimer();
    }

    public void ChangeLevel(int num) => currentLevel=num;

}

