using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour, IGunManager
{
    #region 宣言の折り畳み
    IStateChanger stateChanger;
    ICombo combo;
    [SerializeField] GameObject LGun, RGun;
    [SerializeField] Transform LGun_trans, LGun_trajectory, LGun_Trigger;//左の銃の位置・軌道位置・トリガー
    [SerializeField] Transform RGun_trans, RGun_trajectory, RGun_Trigger;//右の銃の位置・軌道位置・トリガー
    LineRenderer lineRenderer_L, lineRenderer_R;//レイザーポインター左右
    float StartWidth, EndWidth;//レイザーポインター左右　太さ
    private int bullet_countL, bullet_countR;//左右それぞれの残弾数
    private int layerMask =   1 << 6;//的にのみRayが当たるように
    private bool InfiniteMode, LongRayMode;//パワーアップ時の処理
    private float difference;//レイザーの出る角度調整
    [SerializeField] GameObject GunFire_L, GunFire_R;//撃った際の炎
    [SerializeField] GameObject LeftHandAnchor, RightHandAnchor;//左右の手の回転を取得
    [SerializeField] GameObject trajectory_line;//トレイルのプレハブをアタッチ
    private GameObject OVRCam;

    //銃の色取得
    [SerializeField] MeshRenderer[] RedGuncolor;
    [SerializeField] MeshRenderer[] BlueGuncolor;
    [SerializeField] Material[] GunColor;
    #endregion
    void Start()
    {
        lineRenderer_L = LGun.GetComponent<LineRenderer>();
        lineRenderer_R = RGun.GetComponent<LineRenderer>();
        bullet_countL = 1;
        bullet_countR = 1;
        InfiniteMode = false;//弾無限モードになる　　　　　開発中は常にこのモードにしておく
        LongRayMode = false;//レイザーポイントを長くする
        stateChanger = GameObject.FindGameObjectWithTag("GameController").GetComponent<IStateChanger>();
        combo = GameObject.FindGameObjectWithTag("GameController").GetComponent<ICombo>();
        GunFire_L.SetActive(false);
        GunFire_R.SetActive(false);
        difference = 17.5f;
        OVRCam = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        //レイザーの出る条件や、パワーアップ状態で弾数とレイザーの長さが変化する処理
        GunMode();
        ///コントローラーで操作したときの処理///
        Gun_Controller();//プレイヤーの回転とトリガーを押したときの処理
    }
    public void GunMode()
    {
        lineRenderer_L.startWidth = StartWidth;
        lineRenderer_L.endWidth = EndWidth;
        lineRenderer_R.startWidth = StartWidth;
        lineRenderer_R.endWidth = EndWidth;
        if (bullet_countL == 1)//左の弾があればレイザーが出る
        {
            lineRenderer_L.SetPosition(0, LGun_trans.position);
            lineRenderer_L.SetPosition(1, LGun_trajectory.position);
            for (int num = 0; num < 5; num++)
            {
                RedGuncolor[num].material = GunColor[0];
            }
        }
        else//左の弾がなければレイザーが出る
        {
            lineRenderer_L.SetPosition(0, LGun_trans.position);
            lineRenderer_L.SetPosition(1, LGun_trans.position);
            
            for(int num = 0; num < 5; num++)
            {
                RedGuncolor[num].material = GunColor[2];
            }
        }
        if (bullet_countR == 1)//右の弾があればレイザーが出る
        {
            lineRenderer_R.SetPosition(0, RGun_trans.position);
            lineRenderer_R.SetPosition(1, RGun_trajectory.position);
            for (int num = 0; num < 5; num++)
            {
                BlueGuncolor[num].material = GunColor[1];
            }
        }
        else//右の弾がなければレイザーが出る
        {
            lineRenderer_R.SetPosition(0, RGun_trans.position);
            lineRenderer_R.SetPosition(1, RGun_trans.position);
            for (int num = 0; num < 5; num++)
            {
                BlueGuncolor[num].material = GunColor[2];
            }
        }
        if (InfiniteMode == false)//弾無限モードじゃないときは数字を表記
        {
            GunFire_L.SetActive(false);
            GunFire_R.SetActive(false);
        }
        else//弾無限モードで無限表記
        {
            GunFire_L.SetActive(true);
            GunFire_R.SetActive(true);
        }
        if (LongRayMode == false)//レイザー長いモードじゃないときはレイザーの長さと太さが小さくなる
        {
            StartWidth = 0.0025f;
            EndWidth = 0.0001f;
            LGun_trajectory.localPosition = new Vector3(LGun_trajectory.localPosition.x, 342f, LGun_trajectory.localPosition.z);
            RGun_trajectory.localPosition = new Vector3(RGun_trajectory.localPosition.x, 342f, RGun_trajectory.localPosition.z);
        }
        else//レイザー長いモードのときはレイザーの長さと太さが大きくなる　
        {
            StartWidth = 0.01f;
            EndWidth = 0.01f;
            LGun_trajectory.localPosition = new Vector3(LGun_trajectory.localPosition.x, 8420f, LGun_trajectory.localPosition.z);
            RGun_trajectory.localPosition = new Vector3(RGun_trajectory.localPosition.x, 8420f, RGun_trajectory.localPosition.z);
        }
    }
    public void Gun_Controller()
    { 
        //トリガーを押したときの処理
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))//左トリガーを押したとき
        {
            LGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            if (bullet_countL == 1)
            {
                Instantiate(trajectory_line, LGun_trans.transform.position, LeftHandAnchor.transform.rotation * Quaternion.Euler(difference, 0, 0));
            }
            else
            {
                    SoundManager.Instance.PlaySeByName("se_gun_Dont02", transform.GetChild(0).GetChild(0).GetChild(4).gameObject);
            }
            Ray(0);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))//左トリガーを戻したとき
        {
            LGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))//右トリガーを押したとき
        {
            RGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            if (bullet_countR == 1) { Instantiate(trajectory_line, RGun_trans.transform.position, RightHandAnchor.transform.rotation * Quaternion.Euler(difference, 0, 0)); }
            else
            {
                SoundManager.Instance.PlaySeByName("se_gun_Dont02", transform.GetChild(0).GetChild(0).GetChild(4).gameObject);
            }
            Ray(1);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))//右トリガーを戻したとき
        {
            RGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }


        if (bullet_countL == 0 && bullet_countR == 0 )//両方の弾が0になったとき
        {
            if(stateChanger.currentState == IStateChanger.GameState.Game)
            {
                stateChanger.ChangeState(IStateChanger.GameState.Result);
            }
            
        }

        //右スティックを回したときにプレイヤーが回転する処理
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickLeft))
        {
            OVRCam.transform.Rotate(0, -45f, 0);
            Debug.Log("右のジョイスティックを左へ回す");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickRight))
        {
            OVRCam.transform.Rotate(0, 45f, 0);
            Debug.Log("右のジョイスティックを右へ回す");
        }
    }
    IEnumerator Vibration(int LorR)//当てた時の振動処理
    {
        if (LorR == 0)
        {
            OVRInput.SetControllerVibration(1.0f, 0.6f, OVRInput.Controller.LTouch);
            yield return new WaitForSeconds(0.1f);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
        if(LorR == 1)
        {
            OVRInput.SetControllerVibration(1.0f, 0.6f, OVRInput.Controller.RTouch);
            yield return new WaitForSeconds(0.1f);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }


      public void Ray(int LorR)//トリガーが押されたとき 　引数は左か右か
    {
        //当たった的の種類を確認する用のスクリプト
        Ray ray_L = new Ray(LGun_trans.position, LGun_trajectory.position - LGun_trans.position);
        Ray ray_R = new Ray(RGun_trans.position, RGun_trajectory.position - RGun_trans.position);
        RaycastHit hitobj;
        if (LorR == 0)//左トリガーを押したとき(左銃を撃った時)
        {
            if (Physics.Raycast(ray_L, out hitobj, 200, layerMask))//左が的またはスタートボタンのUIに当たったとき
            {
                Hit(0, hitobj);
            }
            else//左が何にも触れていない場合　弾数0&コンボリセット
            {
                if (stateChanger.currentState == IStateChanger.GameState.Game)//プレイモードだったとき
                {
                    if (InfiniteMode) return;
                   SoundManager.Instance.PlaySeByName("se_gun_Dont02", transform.GetChild(0).GetChild(0).GetChild(4).gameObject);
                    bullet_countL = 0;
                    ResetCombo();
                }
            }
        }

        if (LorR == 1)//右トリガーを押したとき(右銃を撃った時)
        {
            if (Physics.Raycast(ray_R, out hitobj, 200, layerMask))//右が的またはスタートボタンのUIに当たったとき
            {
                Hit(1, hitobj);
            }
            else //右が何にも触れていない場合　弾数0 & コンボリセット
            {
                if (stateChanger.currentState == IStateChanger.GameState.Game)//プレイモードだったとき
                {
                    if (InfiniteMode) return;
                    SoundManager.Instance.PlaySeByName("se_gun_Dont02", transform.GetChild(0).GetChild(0).GetChild(5).gameObject);
                    bullet_countR = 0;
                    ResetCombo();
                }
            }
        }
    }
    public void Hit(int LorR, RaycastHit hitobj)//射撃時に的に当たったときに呼ばれる関数
    {
        if (LorR == 0)
        {
            if (stateChanger.currentState == IStateChanger.GameState.Title)//プレイモードだったとき
            {
                if (hitobj.collider.gameObject.layer == 6)//左銃でスタート的を当てた時
                {
                    StartCoroutine(Vibration(0));
                }
            }
            if (stateChanger.currentState == IStateChanger.GameState.Game)//プレイモードだったとき
            {
                if (hitobj.collider.gameObject.layer == 6 && bullet_countL == 1)//左銃で的を当てた時
                {

                    var TargetColor = hitobj.collider.gameObject.GetComponentInParent<TargetInformation>().color;//ターゲットの色
                                                                                                                 //的の色　0が赤　1が青　2が灰色  3お助け的
                    if (TargetColor == 1)
                    {
                        SoundManager.Instance.PlaySeByName("se_nogood11");
                        if (InfiniteMode) return;
                        bullet_countL = 0;
                        ResetCombo();
                    }
                    else
                    {
                        StartCoroutine(Vibration(0));
                    }
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);//俺の銃の色が引数
                }
                else
                {
                    if (InfiniteMode) return;
                    bullet_countL = 0;
                    ResetCombo();
                }
            }
            else
            {
                Reload();
                if (hitobj.collider.gameObject.layer == 6)
                {
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);
                }
            }
        }
        else
        {
            if (stateChanger.currentState == IStateChanger.GameState.Title)//プレイモードだったとき
            {
                if (hitobj.collider.gameObject.layer == 6)//左銃でスタート的を当てた時
                {
                    StartCoroutine(Vibration(1));
                }
            }
            if (stateChanger.currentState == IStateChanger.GameState.Game)//プレイモードだったとき
            {
                if (hitobj.collider.gameObject.layer == 6 && bullet_countR == 1)//右銃で的を当てた時
                {
                    var TargetColor = hitobj.collider.gameObject.GetComponentInParent<TargetInformation>().color;//ターゲットの色
                    if (TargetColor == 0)
                    {
                        SoundManager.Instance.PlaySeByName("se_nogood11", gameObject);
                        if (InfiniteMode) return;
                        bullet_countR = 0;
                        ResetCombo();
                    }
                    else
                    {
                        StartCoroutine(Vibration(1));
                    }
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(2);//俺の銃の色が引数
                }
                else//的の色が青だったら弾が0になる
                {
                    if (InfiniteMode) return;
                    bullet_countR = 0;
                    ResetCombo();
                }
            }
            else
            {
                Reload();
                if (hitobj.collider.gameObject.layer == 6)
                {
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);
                }
            }
        }
       
    }

    public void Reload()
    {
        bullet_countL = 1;
        bullet_countR = 1;
    }
    public void PowerUp()
    {
        InfiniteMode = true;
        LongRayMode = true;
        SoundManager.Instance.PlaySeByName("GunReload", gameObject);
    }
    public void PowerDown()
    {
        InfiniteMode = false;
        LongRayMode = false;
        bullet_countL = 1;
        bullet_countR = 1;

    }
    public void ResetCombo()
    {
        combo.ResetCombo();
    }
    public void GameOver()
    {
    }
}





