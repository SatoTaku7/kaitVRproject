using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingUI : MonoBehaviour, IGunBreakTarget
{
    IStateChanger stateChanger;
    void Start()
    {
        stateChanger = GameObject.FindGameObjectWithTag("GameController").GetComponent<IStateChanger>();
    }


     public void BreakTarget(int color)
    {
        stateChanger.ChangeState(IStateChanger.GameState.Title);
    }
}
