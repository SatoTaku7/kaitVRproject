using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceManager : MonoBehaviour, IPerformanceManager
{
 
    //現在のコンボ状態
    public IPerformanceManager.ComboState currentComboState { get; private set; }
    /// <summary>
    /// コンボ状態が変わるときのコールバック　OnChangeState.Invoke()で呼び出し
    /// </summary>
    public event System.Action OnChangeState;
    public void ChangeState(IPerformanceManager.ComboState nextState)
    {
        currentComboState = nextState;
        OnChangeState.Invoke();
    }
    public void  ComboPerformance(int combo)
    {
        if (combo > 10)
        {
            ChangeState(IPerformanceManager.ComboState.ComboMax);
        }
        else if (combo >= 5)
        {
            ChangeState(IPerformanceManager.ComboState.ComboMid);
        }
        else
        {
            ChangeState(IPerformanceManager.ComboState.ComboLow);
        }
    }
    //ゲームオーバーになったときの処理
    public void GameOver()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

}
