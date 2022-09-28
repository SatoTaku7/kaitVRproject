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
        if (gunManager.ButtonClicked)//スタートボタンが押されたらボタンが消える
        {
            Debug.Log("ボタンクリック:"+gunManager.ButtonClicked);
            if (Button.gameObject.name == gunManager.ButtonName)
            {
                Debug.Log("ボタン名:" + gunManager.ButtonClicked);
                Button.gameObject.SetActive(false);
                gunManager.is_playmode = true;
                gunManager.is_game_over = false;
            }
        }

        if (stateChanger.currentState == IStateChanger.GameState.Title)//ゲームオーバーが呼ばれたらスタートボタンを出現
        {
            Button.gameObject.SetActive(true);
            gunManager.is_playmode = false;
        }
    }
}
