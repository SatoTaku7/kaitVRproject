using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ranking : MonoBehaviour
{
    public GameObject rank_object = null; // Textオブジェクト

    public int[] higher = new int[4];
    //public int[] higher = { 0, 0, 0, 0 };
    // Start is called before the first frame update
    void Start()
    {
        rank_object = GameObject.FindWithTag("rankUI");
        higher[0] = PlayerPrefs.GetInt("First", 0);
        higher[1] = PlayerPrefs.GetInt("Second", 0);
        higher[2] = PlayerPrefs.GetInt("Third", 0);
        rankscore();
        PlayerPrefs.SetInt("First", higher[0]);
        PlayerPrefs.SetInt("Second", higher[1]);
        PlayerPrefs.SetInt("Third", higher[2]);
        Debug.Log("1位" + higher[0]);
    }

    public void uketori(ref int sco)
    {
        //スコアを受け取り
    }

    public void rankscore()
    {

        //higher[0] = 10;
        //higher[1] = 8;
        //higher[2] = 5;
        higher[3] = 8;

        if (higher[3] >= higher[0])
        {
            higher[2] = higher[1];
            higher[1] = higher[0];
            higher[0] = higher[3];
        }

        else if (higher[3] >= higher[1])
        {
            higher[2] = higher[1];
            higher[1] = higher[3];
        }

        else if (higher[3] >= higher[2])
        {
            higher[2] = higher[3];
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
        TextMeshProUGUI rank_text = rank_object.GetComponent<TextMeshProUGUI>();
        // テキストの表示を入れ替える
        rank_text.text = "First:" + higher[0] + "\nSecond:" + higher[1] + "\nThird:" + higher[2];
    }
}
