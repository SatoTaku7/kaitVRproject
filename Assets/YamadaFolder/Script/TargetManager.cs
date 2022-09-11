using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] GameObject TartgetRed;
    [SerializeField] GameObject TartgetBlue;
    [SerializeField] GameObject TartgetGray;

    public int d = 60;//生成距離
    public int n = 40;//生成位置のバラつき　大きいほど中央寄り

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            HitTarget(i, 2, 20);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var g = GameObject.FindWithTag("Target").GetComponentsInParent<TargetBreak>();
            int ran = Random.Range(0, g.Length);
            g[ran].destroyObject();
        }
    }

    public void HitTarget(int num, int color, float size)
    {
        GenerateTartget(num, color, size);
    }

    public void GenerateTartget(int num, int color, float size)
    {
        //座標の決定
        var theta = Random.Range(-Mathf.PI, Mathf.PI);
        var p = Random.Range(0f, 1f);
        var phi = Mathf.Asin(Mathf.Pow(p, 1f / (n + 1f)));

        var x = Random.Range(-50f, 50f);
        var y = Random.Range(0f, 20f);

        var Transform = new Vector3(
                    Mathf.Cos(phi) * Mathf.Cos(theta) * d + x,
                    Mathf.Cos(phi) * Mathf.Sin(theta) * d + y,
                    Mathf.Sin(phi) * (d + num * 10));
        var Rotation = Quaternion.identity;


        //色の決定
        GameObject ins;
        switch (color)
        {
            case 0:
                ins = Instantiate(TartgetRed, Transform, Rotation, this.gameObject.transform);
                ins.transform.localScale = new Vector3(size * 100f, size * 100f, size * 100f);
                break;
            case 1:
                ins = Instantiate(TartgetBlue, Transform, Rotation, this.gameObject.transform);
                ins.transform.localScale = new Vector3(size * 100f, size * 100f, size * 100f);
                break;
            case 2:
                ins = Instantiate(TartgetGray, Transform, Rotation, this.gameObject.transform);
                ins.transform.localScale = new Vector3(size * 100f, size * 100f, size * 100f);
                break;
            default:
                Debug.Log("Targetのcolor決めが出来てません");
                break;
        }
    }
}
