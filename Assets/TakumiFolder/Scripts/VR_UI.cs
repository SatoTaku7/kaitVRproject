using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_UI : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private GunManager gunManager;
    [SerializeField] GameObject Button;
    IStateChanger stateChanger;

    void Start()
    {
        gunManager = Player.GetComponent<GunManager>();
        stateChanger = GameObject.FindGameObjectWithTag("GameController").GetComponent<IStateChanger>();
    }


    void Update()
    {
        if (stateChanger.currentState == IStateChanger.GameState.Title)//ゲームオーバーが呼ばれたらスタートボタンを出現
        {
            Button.gameObject.SetActive(true);//タイトルの時はボタンを表示
            ButtonClicked();//ボタンのクリックを検知した場合の関数…ボタンが非表示になりゲームのStateを変更する
        }
    }

    void ButtonClicked()
    {
        //if (gunManager.ButtonClicked==true)//スタートボタンが押されたらボタンが消える
        //{
        //    if (Button.gameObject.name == gunManager.ButtonName)//クリックしたものがスタートボタンだったとき　ボタンが消えてゲームのStateが"Game"になる
        //    {
        //        // Debug.Log("ボタン名:" + gunManager.ButtonClicked);
        //        Button.gameObject.SetActive(false);
        //        stateChanger.ChangeState(IStateChanger.GameState.Game);
        //    }
        //}
    }
}
