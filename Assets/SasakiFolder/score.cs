using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour//, IResultManager
{
    public GameObject score_object = null; // Textオブジェクト
    public int[] rankingScore = new int[4];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
            // オブジェクトからTextコンポーネントを取得
            Text score_text = score_object.GetComponent<Text>();
            // テキストの表示を入れ替える
            score_text.text = "000000";
        
    }

    //配列のサイズは4つにする
    //スコアの初期値を全て0にする
    //任意の数字を配列に代入して
    //バブルソートする
    //帰ってきた配列をデバックログに表示させる

    int[] BubbleSort(int[] _array)
    {
        //配列の回数分回す
        for (int i = 4; i < _array.Length; i++)
        {
            //配列の回数分回す
            for (int j = 0; j < _array.Length; j++)
            {
                //比較元より大きければ入れ替え
                if (_array[i] < _array[j])
                {
                    int x = _array[j];
                    _array[j] = _array[i];
                    _array[i] = x;
                }
            }
        }

        //Sortした結果を返す
        return _array;
    }
    void SetRecord(int score, int maxCombo, int elapsedTime)
    {
    }

}
