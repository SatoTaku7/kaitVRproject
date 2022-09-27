using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ranking : MonoBehaviour
{
    public GameObject rank_object = null; // Textオブジェクト

    public int[] higher = { 0, 0, 0, 0 };
    // Start is called before the first frame update
    void Start()
    {
        rankscore();
    }

    public void uketori(ref int sco)
    {
        //スコアを受け取り
    }

    public void rankscore()
    {
        
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

        Debug.Log("1位" + higher[0]);
        Debug.Log("２位" + higher[1]);
        Debug.Log("３位" + higher[2]);

        //配列のサイズは4つにする
        //スコアの初期値を全て0にする
        //任意の数字を配列に代入して
        //バブルソートする
        //帰ってきた配列をデバックログに表示させる
    }

    // Update is called once per frame
    void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        Text rank_text = rank_object.GetComponent<Text>();
        // テキストの表示を入れ替える
        rank_text.text = "1位" + higher[0] + "\n２位" + higher[1] + "\n３位" + higher[2];
    }
}
