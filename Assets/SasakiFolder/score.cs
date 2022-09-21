using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public GameObject score_object = null; // Textオブジェクト
    public int score_num = 0; // スコア変数

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            // オブジェクトからTextコンポーネントを取得
            Text score_text = score_object.GetComponent<Text>();
            // テキストの表示を入れ替える
            score_text.text = "000000";
        }
}
