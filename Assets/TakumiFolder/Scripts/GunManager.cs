using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour, IGunManager
{
    IStateChanger stateChanger;
    ICombo combo;
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

    //色の情報
    TargetInformation targetInformation;


    void Start()
    {
        lineRenderer_L = LGun.GetComponent<LineRenderer>();
        lineRenderer_R = RGun.GetComponent<LineRenderer>();
        bullet_countL = 1;
        bullet_countR = 1;
        ButtonClicked = false;
        InfiniteMode = false;//弾無限モードになる　　　　　開発中は常にこのモードにしておく
        LongRayMode = false;//レイザーポイントを長くする
        stateChanger = GameObject.FindGameObjectWithTag("GameController").GetComponent<IStateChanger>();
        combo= GameObject.FindGameObjectWithTag("GameController").GetComponent<ICombo>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer_L.startWidth = StartWidth;
        lineRenderer_L.endWidth = EndWidth;
        lineRenderer_R.startWidth = StartWidth;
        lineRenderer_R.endWidth = EndWidth;
        lineRenderer_L.SetPosition(0, LGun_trans.position);
        lineRenderer_R.SetPosition(0, RGun_trans.position);
        lineRenderer_L.SetPosition(1, LGun_trajectory.position);
        lineRenderer_R.SetPosition(1, RGun_trajectory.position);
        if (InfiniteMode == false)//弾無限モードじゃないときは数字を表記
        {
            textbullet_countL.text = bullet_countL.ToString();
            textbullet_countR.text = bullet_countR.ToString();
        }
        else//弾無限モードで無限表記
        {
            textbullet_countL.text = "∞";
            textbullet_countR.text = "∞";
        }
     　 if (LongRayMode == false)//レイザー長いモードじゃないときはレイザーの長さと太さが小さくなる
     　 {
            StartWidth = 0.001f;
            EndWidth = 0.0001f;
            LGun_trajectory.localPosition = new Vector3(LGun_trajectory.localPosition.x, 342f, LGun_trajectory.localPosition.z);
            RGun_trajectory.localPosition = new Vector3(RGun_trajectory.localPosition.x, 342f, RGun_trajectory.localPosition.z);
      　}
      　else//レイザー長いモードのときはレイザーの長さと太さが大きくなる　
        {
            StartWidth = 0.01f;
            EndWidth = 0.01f;
            LGun_trajectory.localPosition = new Vector3(LGun_trajectory.localPosition.x,8420f, LGun_trajectory.localPosition.z);
            RGun_trajectory.localPosition = new Vector3(RGun_trajectory.localPosition.x, 8420f, RGun_trajectory.localPosition.z);
        }
           
        
        ///ここから下はプレイヤーがコントローラーで操作したときの処理///
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
        //FIXME:無限モード中に両方で弾を外すと的うちゲームが終了する
        if (bullet_countL == 0 && bullet_countR == 0&&stateChanger.currentState==IStateChanger.GameState.Game)//両方の弾が0になったとき
        {
            stateChanger.ChangeState(IStateChanger.GameState.Result);

        }
        PlayerRotate();//プレイヤーの回転
    }
    public void PlayerRotate()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
        {
            this.gameObject.transform.Rotate(0, -45f, 0);
            Debug.Log("左のジョイスティックを左へ回す");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight))
        {
            this.gameObject.transform.Rotate(0, 45f, 0);
            Debug.Log("左のジョイスティックを右へ回す");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickLeft))
        {
            this.gameObject.transform.Rotate(0, -45f, 0);
            Debug.Log("右のジョイスティックを左へ回す");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickRight))
        {
            this.gameObject.transform.Rotate(0, 45f, 0);
            Debug.Log("右のジョイスティックを右へ回す");
        }
    }
    public void Ray(int LorR)//トリガーが押されたとき 　引数は左か右か
    {
        //当たった的の種類を確認する用のスクリプト
        Ray ray_L = new Ray(LGun_trans.position, LGun_trajectory.position - LGun_trans.position);
        Ray ray_R = new Ray(RGun_trans.position, RGun_trajectory.position - RGun_trans.position);
        RaycastHit hitobj;
        if (LorR == 1)//左トリガーを押したとき(左銃を撃った時)
        {
            if (Physics.Raycast(ray_L, out hitobj, 200, layerMask))//左が的またはスタートボタンのUIに当たったとき
            {
                if (hitobj.collider.gameObject.layer == 7)//UIのボタンに当たったとき
                {
                    ButtonName = hitobj.collider.gameObject.name;//UIの名前を取得
                    bullet_countL = 1;
                    bullet_countR = 1;
                    Debug.Log("UI" + hitobj.collider.gameObject.name + "を確認。弾数を1にします。");
                }//CHANGED:boolではなくstateChangerのcurrentStateを参照するように変更
                if (stateChanger.currentState == IStateChanger.GameState.Game)//プレイモードだったとき
                {
                    
                    if (hitobj.collider.gameObject.layer == 6 && bullet_countL == 1)//左銃で的を当てた時
                    {
                        var TargetColor = hitobj.collider.gameObject.GetComponentInParent<TargetInformation>().color;//ターゲットの色
                        //的の色　0が赤　1が青　2が灰色  3お助け的
                        if(TargetColor == 1)
                        {
                            if (InfiniteMode) return;
                            bullet_countL = 0;
                            ResetCombo();
                        }
                        //FIXME:お助けマトを撃った時を撃った時エラーがでる　また以下のようなコードで判別するとインターフェースを利用する意味が無くなることに注意
                        hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(1);//俺の銃の色が引数
                    }
                    else
                    {
                        if (InfiniteMode) return;
                        bullet_countL = 0;
                        ResetCombo();
                    }

                }
            }
            else//左が何にも触れていない場合　弾数0&コンボリセット
            {
                if (stateChanger.currentState == IStateChanger.GameState.Game)//プレイモードだったとき
                {
                    if (InfiniteMode) return;
                    bullet_countL = 0;
                    ResetCombo();
                }
            }
        }

        if (LorR == 2)//右トリガーを押したとき(右銃を撃った時)
        {
            if (Physics.Raycast(ray_R, out hitobj, 200, layerMask))//右が的またはスタートボタンのUIに当たったとき
            {
                if (hitobj.collider.gameObject.layer == 7)//UIのボタンに当たったとき
                {
                    ButtonName = hitobj.collider.gameObject.name;
                    bullet_countL = 1;
                    bullet_countR = 1;
                    Debug.Log("UI" + hitobj.collider.gameObject.name + "を確認。弾数を1にします。");
                }
                //CHANGED:boolではなくstateChangerのcurrentStateを参照するように変更
                if (stateChanger.currentState==IStateChanger.GameState.Game)//プレイモードだったとき
                {
                    if (hitobj.collider.gameObject.layer == 6 && bullet_countR == 1)//右銃で的を当てた時
                    {
                        var TargetColor = hitobj.collider.gameObject.GetComponentInParent<TargetInformation>().color;//ターゲットの色
                        if (TargetColor == 0)
                        {
                            if (InfiniteMode) return;
                            bullet_countR = 0;
                            ResetCombo();
                        }
                        //FIXME:お助けマトを撃った時を撃った時エラーがでる　また以下のようなコードで判別するとインターフェースを利用する意味が無くなることに注意
                        hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(2);//俺の銃の色が引数
                    }
                    else//的の色が青だったら弾が0になる
                    {
                        if (InfiniteMode) return;
                        bullet_countR = 0;
                        ResetCombo();
                    }
                }
            }
            else //右が何にも触れていない場合　弾数0 & コンボリセット
            {
                if (stateChanger.currentState == IStateChanger.GameState.Game)//プレイモードだったとき
                {
                    if (InfiniteMode) return;
                    bullet_countR = 0;
                    ResetCombo();
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
   




