using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour, IGunManager
{
    [SerializeField] GameObject LGun, RGun;
    [SerializeField] Transform LGun_trans, LGun_trajectory,LGun_Trigger;//左の銃の位置・軌道位置・トリガー
    [SerializeField] Transform RGun_trans, RGun_trajectory, RGun_Trigger;//右の銃の位置・軌道位置・トリガー
    LineRenderer lineRenderer_L,lineRenderer_R;//レイザーポインター左右
    [SerializeField] float StartWidth, EndWidth;//レイザーポインター左右　太さ
    [SerializeField] Text textbullet_countL, textbullet_countR;//仮の残弾数UI左右
    private int bullet_countL, bullet_countR;//左右それぞれの残弾数
    private int hit;
    private int layerMask=1<<7|1<<6;
    [SerializeField] bool InfiniteMode;

    public  string ButtonName;//クリックされたボタンの名前を参照　oculus標準の機能が使えなかったので別のやり方で代替
    public  bool ButtonClicked;//ボタンがクリックされたかどうか　oculus標準の機能が使えなかったので別のやり方で代替

    void Start()
    {
        lineRenderer_L =LGun.GetComponent<LineRenderer>();
        lineRenderer_R = RGun.GetComponent<LineRenderer>();
        bullet_countL = 1;
        bullet_countR = 1;
        hit = 0;
        lineRenderer_L.startWidth = StartWidth;
        lineRenderer_L.endWidth = EndWidth;
        lineRenderer_R.startWidth = StartWidth;
        lineRenderer_R.endWidth = EndWidth;
        ButtonClicked = false;
        //InfiniteMode = false;
        
    }

    // Update is called once per frame
    void Update()
    {
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
        
        lineRenderer_L.SetPosition(0, LGun_trans.position);
        lineRenderer_L.SetPosition(1, LGun_trajectory.position) ;
        lineRenderer_R.SetPosition(0, RGun_trans.position);
        lineRenderer_R.SetPosition(1, RGun_trajectory.position);
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))//左トリガーを押したとき
        {
            if (InfiniteMode == false)
            {
                bullet_countL = 0;
            }   
            LGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            Ray();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))//左トリガーを戻したとき
        {
            LGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
            ButtonClicked = false;
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))//右トリガーを押したとき
        {
            if (InfiniteMode == false)
            {
                bullet_countL = 0;
            }
            RGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            Ray();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))//右トリガーを戻したとき
        {
            RGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
            ButtonClicked = false;
        } 
    }
    public void Ray()
    {
        //当たった的の種類を確認する用のスクリプト
        Ray ray_L = new Ray(LGun_trans.position, LGun_trajectory.position - LGun_trans.position);
        Ray ray_R = new Ray(RGun_trans.position, RGun_trajectory.position - RGun_trans.position);
        RaycastHit hitobj;
        if (Physics.Raycast(ray_L, out hitobj, 200, layerMask))
        {
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))//左トリガーを押したとき
            {
                if (hitobj.collider.gameObject.layer == 7)//UIのボタンの時
                {
                    ButtonName = hitobj.collider.gameObject.name;
                    ButtonClicked = true;
                    //Debug.Log("UIを確認");
                }
                if (hitobj.collider.gameObject.layer == 6 && (bullet_countL == 1 || InfiniteMode == true))//的に当たったとき
                {
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(hit);
                    Reload();
                    Debug.Log(hitobj.collider.gameObject.name + ":衝突したオブジェクト");
                }
            }
            Debug.DrawRay(LGun_trans.position, LGun_trajectory.position - LGun_trans.position, Color.red);
            //Debug.Log(hitobj.collider.gameObject.name);
        }
        if (Physics.Raycast(ray_R, out hitobj, 200, layerMask))
        {
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))//右トリガーを押したとき
            {

                if (hitobj.collider.gameObject.layer == 7)//UIのボタンの時
                {
                    ButtonName = hitobj.collider.gameObject.name;
                    ButtonClicked = true;
                    //Debug.Log("UIを確認");
                }
                if (hitobj.collider.gameObject.layer == 6 && (bullet_countR == 1 || InfiniteMode == true))//的に当たったとき
                {
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(hit);
                    Reload();
                    Debug.Log(hitobj.collider.gameObject.name + ":衝突したオブジェクト");
                }
            }
            Debug.DrawRay(RGun_trans.position, RGun_trajectory.position - RGun_trans.position, Color.red);
            //Debug.Log(hitobj.collider.gameObject.name);
        }
       
    }

    public  void Reload()
    { 
        bullet_countL = 1;
        bullet_countR = 1;
        hit = 0;
    }
    public void PowerUp()
    {

    }
    public void GameOver()
    {

    }
}


    

