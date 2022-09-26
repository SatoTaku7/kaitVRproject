
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGameManager : MonoBehaviour, IStateChanger, IBreakTargetChecker
{
    //銃のマネージャーを参照して取得する必要がある。
    IGunManager gunManager;
    IResultManager resultManager;
    private int score = 0;
    private int combo = 0;
    private int maxCombo = 0;
    IPerformanceManager performanceManager;
    ITimer timer;
    ITargetManager targetManager;
    public float countDownTimer { get; private set; }
    public IStateChanger.GameState currentState { get; private set; }
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


        }
        else if (currentState == IStateChanger.GameState.Result)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
               
                Debug.Log("タイトル戻ります。");
                resultManager?.SetRecord(score, maxCombo, (int)timer.playTime);
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
    IEnumerator StartInterval()
    {
        yield return new WaitForSeconds(3f);
        timer.StartPlay();

    }
}

