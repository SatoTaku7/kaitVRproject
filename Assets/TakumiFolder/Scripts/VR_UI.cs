using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_UI : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private  GunManager gunManager;
    [SerializeField] GameObject Button;

    void Start()
    {
        gunManager = Player.GetComponent<GunManager>();
    }


    void Update()
    {
        if (gunManager.ButtonClicked)//スタートボタンが押されたらCanvasが消える
        {
            if (Button.gameObject.name == gunManager.ButtonName)
            {
                Button.gameObject.SetActive(false);
            }
        }
        if (gunManager.is_game_over)//ゲームオーバーが呼ばれたらスタートボタンを出現
        {
            Button.gameObject.SetActive(true);
            gunManager.ButtonClicked = false;
        }
    }
}
