﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGameManager : MonoBehaviour, IStateChanger, ILevelState, IBreakTargetChecker
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
    [SerializeField]AssistManager assistManager;

    private GameObject currentScoreUICanvas;
    private currentScoreText _scoreText;
    /// <summary>
    /// ステータスを変更するときに呼び出す
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeState(IStateChanger.GameState nextState)
    {
        if (currentState != nextState)
        {
            currentState = nextState;
            if (nextState == IStateChanger.GameState.Title)
            {
                resultManager.DisableUI();
                scoreSum = 0;
                maxCombo = 0;
                timer.ResetPlayTime();
                timer.SetTimer(10f);
                timer.ResetTimer();

            }
            else if (nextState == IStateChanger.GameState.Game)
            {
                //ゲーム開始時の初期化処理はここに書く
                gunManager.Reload();
                targetManager.TargetInit();
                timer.StartPlay();
                //StartCoroutine(StartInterval());
            }
            else if (nextState == IStateChanger.GameState.Result)
            {
                //ゲーム終了時の処理はここに書く
                targetManager.AllTargetDestroy();
                timer.StopPlay();
                resultManager.SetRecord(scoreSum, maxCombo, (int)timer.playTime);
                resultManager.EnableUI();
                gunManager.PowerDown();
            }
            
        }

    }
    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<ITimer>();
        resultManager = GetComponent<IResultManager>();
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<IGunManager>();
        targetManager = GetComponent<ITargetManager>();
        performanceManager = GetComponent<IPerformanceManager>();
        comboManager = GetComponent<ICombo>();
        ChangeState(IStateChanger.GameState.Title);
        currentScoreUICanvas = GameObject.FindGameObjectWithTag("currentScoreUI");
        _scoreText = currentScoreUICanvas.GetComponentInChildren<currentScoreText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == IStateChanger.GameState.Title)
        {
        }
        else if (currentState == IStateChanger.GameState.Game)
        {

            MaxCombo(comboManager.combo);
            if (scoreSum >= 22800)
            {
                ChangeLevel(7);
            }
            else if(scoreSum >= 16800)
            {
                ChangeLevel(6);
                timer.SetTimer(4);
            }
            else if(scoreSum >= 11600)
            {
                if (currentLevel != 5)
                {
                    assistManager.GenerateAssistTarget();
                }
                ChangeLevel(5);
            }
            else if (scoreSum >= 8800)
            {
                ChangeLevel(4);
            }
            else if(scoreSum >= 4000)
            {
                if (currentLevel != 3)
                {
                    assistManager.GenerateAssistTarget();
                }
                ChangeLevel(3);
                timer.SetTimer(6);
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

        }
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
    //Changed:スコアと割れた時の処理を分割した
    public void ScoreUpdate(int addScore)
    {
        scoreSum += addScore;
        _scoreText.SlideToNumber(scoreSum, 0.5f);
    }

    public void BreakTarget()
    {
        timer.ResetTimer();
        gunManager.Reload();
    }

    public void ChangeLevel(int num) => currentLevel=num;

    public void BreakAssistTarget()
    {
        StartCoroutine(PowerUpTime());
    }
    IEnumerator PowerUpTime()
    {
        gunManager.PowerUp();
        yield return new WaitForSeconds(10f);
        gunManager.PowerDown();
    }
}


