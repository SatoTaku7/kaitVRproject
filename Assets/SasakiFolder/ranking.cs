using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ranking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void rankscore()
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
        
    }
}
