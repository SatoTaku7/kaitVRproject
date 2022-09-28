using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour, IResultManager
{
    public GameObject score_object = null; // Textオブジェクト
    public int score_num = 0; // スコア変数
    public int score { get; private set; }
    public int maxCombo { get; private set; }
    public int elapsedTime { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        score_object = GameObject.FindWithTag("scoreUI");
        //スコアをもらう
        int mysc;
        mysc = 99;//playerScript.BulletCount;
        Debug.Log("スコア"+mysc+"点");
    }

    /*public void rankscore()
    {
        int[] higher = { 0, 0, 0, 0 };
        //スコアをもらう
        //higher[3]=playerScript.BulletCount;

        if (higher[3] >= higher[2])
        {
            higher[2] = higher[3];
        }

        else if (higher[3] >= higher[1])
        {
            higher[2] = higher[1];
            higher[1] = higher[3];
        }

        else if (higher[3] >= higher[0])
        {
            higher[2] = higher[1];
            higher[1] = higher[0];
            higher[0] = higher[3];
        }
        else { };

        Debug.Log("1位"+higher[0]);
        Debug.Log("２位"+higher[1]);
        Debug.Log("３位"+higher[2]);

        //配列のサイズは4つにする
        //スコアの初期値を全て0にする
        //任意の数字を配列に代入して
        //バブルソートする
        //帰ってきた配列をデバックログに表示させる
    }*/

    // Update is called once per frame
    void Update()
    {
        //SetRecord(100,00,1);
    }

    public void SetRecord(int score, int maxCombo, int elapsedTime)
    {
        // オブジェクトからTextコンポーネントを取得
        TextMeshProUGUI score_text = score_object.GetComponent<TextMeshProUGUI>();
        // テキストの表示を入れ替える
        score_text.text = "Score:" + score;
    }
}