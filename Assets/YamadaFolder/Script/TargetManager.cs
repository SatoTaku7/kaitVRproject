using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour,ITargetManager
{
    public int breakTargetCount { get; private set; }

    [SerializeField] GameObject TartgetRed;
    [SerializeField] GameObject TartgetBlue;
    [SerializeField] GameObject TartgetGray;

    public int d = 60;//生成距離
    public int n = 40;//生成位置のバラつき　大きいほど中央寄り

    public int targetSize = 15;//的のサイズ
    public float minusSize = 4f;

    public int level;//デバッグ用のレベル
    public int breakNum = 0;//デバッグ用の破壊的数

    private int colorLevel;//カラー用のレベル->灰色を壊さないまま次のレベルに行った時用
    //レベルが変わってから最初の破壊時に呼ぶ用
    [SerializeField] bool firstBreak1 = true;
    [SerializeField] bool firstBreak2 = true;
    [SerializeField] bool firstBreak3 = true;
    [SerializeField] bool firstBreak4 = true;

    //ポップアップするスコアテキスト
    [SerializeField] GameObject PopUpScoreText;

    //ポップアップするスコアテキスト
    [SerializeField] GameObject[] BreakEffectObj = new GameObject[3];

    private GameObject GameManager;
    private ICombo _combo;
    private IBreakTargetChecker breakTargetChecker;

    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameController");
        _combo = GameManager.GetComponent<ICombo>();
        breakTargetChecker= GameManager.GetComponent<IBreakTargetChecker>();
    }

    public void TargetInit()
    {
        for (int i = 0; i < 4; i++)
        {
            GenerateTartget(i, 2, targetSize);
        }
    }
    public void AllTargetDestroy()
    {
        //生成済みの的が全部消える処理よろしくお願いします。
        foreach (Transform child in this.gameObject.transform)
        {
            GameObject childObject = child.gameObject;
            Destroy(childObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var g = GameObject.FindWithTag("Target").GetComponentsInChildren<IGunBreakTarget>();
            int ran = Random.Range(0, g.Length);
            int ranGun = Random.Range(1, 3);
            g[ran].BreakTarget(ranGun);
        }
        level = 1 + breakNum / 10;
    }

    public void HitTarget(int num, int color, float size,int gunColor, Vector3 pos)
    {
        breakNum++;
        GenerateTartget(num, color, size);
        CalculateScore(color, gunColor, pos);
        BreakEffect(pos);
    }

    //当たった的と銃の情報からスコアの算出
    private void CalculateScore(int color, int gunColor, Vector3 pos)
    {
        //コンボ判定用
        if (color != 2)
        {
            //同色なら
            if (color == 0 && gunColor == 1)
            {
                Debug.Log("コンボを増やします");
                _combo.ContinuousCombo();
            }
            if (color == 1 && gunColor == 2)
            {
                Debug.Log("コンボを増やします");
                _combo.ContinuousCombo();
            }
        }

        //スコアの算出
        int score = _combo.ScoreCal(_combo.combo);
        _combo.UpdateAllScore();
        breakTargetChecker.BreakTarget();
        //スコアをポップアップ
        PopUpScore(color, score, pos);
    }

    //スコアをポップアップさせる
    private void PopUpScore(int color,int score,Vector3 pos)
    {
        GameObject ins;
        PopUpTextController info;
        ins = Instantiate(PopUpScoreText, pos, new Quaternion(0, 0, 0, 0), this.gameObject.transform);
        info = ins.GetComponent<PopUpTextController>();
        info.Init(score, color);
    }

    //的が壊れた際のエフェクト
    private void BreakEffect(Vector3 pos)
    {
        //コンボ数を取得
        int combo = 0;

        //コンボ数に応じた演出
        if (combo > 4)
        {
            Instantiate(BreakEffectObj[1], pos, new Quaternion(0, 0, 0, 0), this.gameObject.transform);
        }
        else if (combo > 9)
        {
            Instantiate(BreakEffectObj[2], pos, new Quaternion(0, 0, 0, 0), this.gameObject.transform);
        }
        else
        {
            Instantiate(BreakEffectObj[0], pos, new Quaternion(0, 0, 0, 0), this.gameObject.transform);
        }
    }

    //新たな的の生成
    public void GenerateTartget(int num, int color, float size)
    {
        //座標の決定 球状生成の場合、中央に寄ってしまう
        /*
        var theta = Random.Range(-Mathf.PI, Mathf.PI);
        var p = Random.Range(0f, 1f);
        var phi = Mathf.Asin(Mathf.Pow(p, 1f / (n + 1f)));

        var x = Random.Range(-50f, 50f);
        var y = Random.Range(0f, 20f);

        var Transform = new Vector3(
                    Mathf.Cos(phi) * Mathf.Cos(theta) * d + x,
                    Mathf.Cos(phi) * Mathf.Sin(theta) * d + y,
                    Mathf.Sin(phi) * (d + num * 20));
        var Rotation = Quaternion.identity;
        */

        //球状ではなく平面上に生成するパターン
        var x = Random.Range(-70f, 70f);
        var y = Random.Range(-10f, 40f);

        var Transform = new Vector3(x, y, d + num * 20);
        var Rotation = Quaternion.identity;

        //レベルの取得


        //レベルに応じたサイズの変更
        size = targetSize + minusSize - Mathf.Min(level, 3) * minusSize;

        //レベルに応じた色レベルの設定
        level = Mathf.Min(level, 5);
        if (level == 1 || level == 2)
            colorLevel = level;
        if (level == 3)
        {
            colorLevel = 2;
            if (!firstBreak1)
                colorLevel = level;
        }
        if (level == 4)
        {
            colorLevel = 2;
            if (!firstBreak1)
                colorLevel = 3;
            if (!firstBreak2)
                colorLevel = level;
        }
        if (level == 5)
        {
            colorLevel = 2;
            if (!firstBreak1)
                colorLevel = 3;
            if (!firstBreak2)
                colorLevel = 4;
            if (!firstBreak3)
                colorLevel = level;
        }

        //レベルに応じた色の設定
        switch (colorLevel)
        {
            case 1:
                color = 2;//灰色
                break;
            case 2:
                if (color == 0)
                    color = 1;
                else if (color == 1)
                    color = 0;
                if (firstBreak1)
                {
                    color = Random.Range(0, 2);//青か赤
                    firstBreak1 = false;
                    Debug.Log("色付き的を1枚増やします");
                }
                break;
            case 3:
                var c = GetComponentsInChildren<TargetInformation>();
                if (firstBreak2 && color == 2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (c[i].color == 0)
                        {
                            color = 1;
                        }
                        if (c[i].color == 1)
                        {
                            color = 0;
                        }
                    }
                    firstBreak2 = false;
                    Debug.Log("色付き的を1枚増やします");
                }
                break;
            case 4:
                if (firstBreak3 && color == 2)
                {
                    color = Random.Range(0, 2);//青か赤
                    firstBreak3 = false;
                    Debug.Log("色付き的を1枚増やします");
                }
                break;
            case 5:
                c = GetComponentsInChildren<TargetInformation>();
                if (firstBreak4 && color == 2)
                {
                    int red = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (c[i].color == 0)
                        {
                            red++;
                        }
                    }
                    if (red == 1)
                    {
                        color = 0;
                    }
                    if (red == 2)
                    {
                        color = 1;
                    }
                    if (red != 1 && red != 2) return;
                    firstBreak4 = false;
                    Debug.Log("色付き的を1枚増やします");
                }
                break;
            default:
                Debug.Log(level+ "->levelの数字がおかしいです");
                break;
        }

        //的の生成、番号の設定
        GameObject ins;
        TargetInformation info;
        switch (color)
        {
            case 0:
                ins = Instantiate(TartgetRed, Transform, Rotation, this.gameObject.transform);
                ins.transform.localScale = new Vector3(size * 100f, size * 100f, size * 100f);
                info = ins.GetComponent<TargetInformation>();
                info.num = num;
                break;
            case 1:
                ins = Instantiate(TartgetBlue, Transform, Rotation, this.gameObject.transform);
                ins.transform.localScale = new Vector3(size * 100f, size * 100f, size * 100f);
                info = ins.GetComponent<TargetInformation>();
                info.num = num;
                break;
            case 2:
                ins = Instantiate(TartgetGray, Transform, Rotation, this.gameObject.transform);
                ins.transform.localScale = new Vector3(size * 100f, size * 100f, size * 100f);
                info = ins.GetComponent<TargetInformation>();
                info.num = num;
                break;
            default:
                Debug.Log("Targetのcolor決めが出来てません");
                break;
        }
    }
}
