using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour, IGunManager
{
    [SerializeField] GameObject LGun, RGun;
    [SerializeField] Transform LGun_trans, LGun_trajectory, LGun_Trigger;//左の銃の位置・軌道位置・トリガー
    [SerializeField] Transform RGun_trans, RGun_trajectory, RGun_Trigger;//右の銃の位置・軌道位置・トリガー
    LineRenderer lineRenderer_L, lineRenderer_R;//レイザーポインター左右
    [SerializeField] float StartWidth, EndWidth;//レイザーポインター左右　太さ
    [SerializeField] Text textbullet_countL, textbullet_countR;//仮の残弾数UI左右
    private int bullet_countL, bullet_countR;//左右それぞれの残弾数
    private int layerMask = 1 << 7 | 1 << 6;
    [SerializeField] bool InfiniteMode, LongRayMode;

    public string ButtonName;//クリックされたボタンの名前を参照　oculus標準の機能が使えなかったので別のやり方で代替
    public bool ButtonClicked;//ボタンがクリックされたかどうか　oculus標準の機能が使えなかったので別のやり方で代替
    public bool is_playmode, is_game_over;//ゲーム中かどうか・ゲームオーバー判定

    void Start()
    {
        lineRenderer_L = LGun.GetComponent<LineRenderer>();
        lineRenderer_R = RGun.GetComponent<LineRenderer>();
        bullet_countL = 1;
        bullet_countR = 1;
        ButtonClicked = false;
        InfiniteMode = false;//弾無限モードになる　　　　　開発中は常にこのモードにしておく
        LongRayMode = true;//レイザーポイントを長くする
        is_playmode = false;
        is_game_over = false;


    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer_L.startWidth = StartWidth;
        lineRenderer_L.endWidth = EndWidth;
        lineRenderer_R.startWidth = StartWidth;
        lineRenderer_R.endWidth = EndWidth;
        if (InfiniteMode == false)//弾無限モードじゃないときは数字を表記
        {
            textbullet_countL.text = bullet_countL.ToString();
            textbullet_countR.text = bullet_countR.ToString();
        }
        else
        {
            textbullet_countL.text = "∞";
            textbullet_countR.text = "∞";
        }
        if (LongRayMode == false)
        {
            StartWidth = 0.001f;
            EndWidth = 0.0001f;
        }
        else
        {
            StartWidth = 0.01f;
            EndWidth = 0.01f;
        }

        lineRenderer_L.SetPosition(0, LGun_trans.position);
        lineRenderer_L.SetPosition(1, LGun_trajectory.position);
        lineRenderer_R.SetPosition(0, RGun_trans.position);
        lineRenderer_R.SetPosition(1, RGun_trajectory.position);
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))//左トリガーを押したとき
        {

            LGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            ButtonClicked = true;
            Ray(1);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))//左トリガーを戻したとき
        {
            LGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
            ButtonClicked = false;
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))//右トリガーを押したとき
        {

            RGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            ButtonClicked = true;
            Ray(2);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))//右トリガーを戻したとき
        {
            RGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
            ButtonClicked = false;
        }

        if (bullet_countL == 0 && bullet_countR == 0)
        {
            GameOver();
        }
        Debug.Log("is_playmode");

    }
    public void Ray(int LorR)//トリガーが押されたとき 引数は左か右か
    {
        //当たった的の種類を確認する用のスクリプト
        Ray ray_L = new Ray(LGun_trans.position, LGun_trajectory.position - LGun_trans.position);
        Ray ray_R = new Ray(RGun_trans.position, RGun_trajectory.position - RGun_trans.position);
        RaycastHit hitobj;
        if (LorR == 1)//左トリガーを押したとき
        {
            if (Physics.Raycast(ray_L, out hitobj, 200, layerMask))//左が当たったとき
            {
                if (hitobj.collider.gameObject.layer == 7)//UIのボタンの時
                {
                    //bullet_countL = 1;//UIをタッチしたときは弾数を1にする、ノーカンの処理を代理でおいといた。バグの元になりそうなのでので後で直します
                    ButtonName = hitobj.collider.gameObject.name;
                    bullet_countL = 1;
                    bullet_countR = 1;
                    Debug.Log("UI" + hitobj.collider.gameObject.name + "を確認。弾数を1にします。");
                }
                if (is_playmode)//プレイモードだったとき
                {
                    if (InfiniteMode == true)//無限モードだったとき
                    {
                        if (hitobj.collider.gameObject.layer == 6)//的に当たったとき
                        {
                            hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);
                            Reload();
                            Debug.Log(hitobj.collider.gameObject.name + ":衝突したオブジェクト");
                        }
                    }
                    else//通常モードだったとき
                    {
                        if (hitobj.collider.gameObject.layer == 6 && bullet_countL == 1)//的に当たったとき
                        {
                            hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);
                            Reload();
                            Debug.Log(hitobj.collider.gameObject.name + ":衝突したオブジェクト");
                        }
                    }
                }
            }
            else bullet_countL = 0;

        }
        if (LorR == 2)//右トリガーを押したとき
        {
            if (Physics.Raycast(ray_R, out hitobj, 200, layerMask))//右が当たったとき
            {
                if (hitobj.collider.gameObject.layer == 7)//UIのボタンの時
                {
                    //bullet_countR = 1;//UIをタッチしたときは弾数を1にする、ノーカンの処理を代理でおいといた。バグの元になりそうなのでので後で直します
                    ButtonName = hitobj.collider.gameObject.name;
                    bullet_countL = 1;
                    bullet_countR = 1;
                    Debug.Log("UI" + hitobj.collider.gameObject.name + "を確認。弾数を1にします。");
                }
                if (is_playmode)//プレイモードだったとき
                {
                    if (InfiniteMode == true)//無限モードだったとき
                    {
                        if (hitobj.collider.gameObject.layer == 6)//的に当たったとき
                        {
                            hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(2);
                            Reload();
                            Debug.Log(hitobj.collider.gameObject.name + ":衝突したオブジェクト");
                        }
                    }
                    else//通常モードだったとき
                    {
                        if (hitobj.collider.gameObject.layer == 6 && bullet_countR == 1)//的に当たったとき
                        {
                            hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(2);
                            Reload();
                            Debug.Log(hitobj.collider.gameObject.name + ":衝突したオブジェクト");
                        }
                    }
                }
            }
            else bullet_countR = 0;
        }
        Debug.DrawRay(LGun_trans.position, LGun_trajectory.position - LGun_trans.position, Color.red);
        Debug.DrawRay(RGun_trans.position, RGun_trajectory.position - RGun_trans.position, Color.red);
    }

    public void Reload()
    {
        bullet_countL = 1;
        bullet_countR = 1;
    }
    public void PowerUp()
    {

    }
    public void GameOver()
    {
        is_game_over = true;
        is_playmode = false;
    }
}




