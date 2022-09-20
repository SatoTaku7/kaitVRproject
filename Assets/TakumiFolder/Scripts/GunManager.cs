using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour
{
    [SerializeField] GameObject LGun, RGun;
    [SerializeField] Transform LGun_trans, LGun_trajectory,LGun_Trigger;//左の銃の位置・軌道位置・トリガー
    [SerializeField] Transform RGun_trans, RGun_trajectory, RGun_Trigger;//右の銃の位置・軌道位置・トリガー
    LineRenderer lineRenderer_L,lineRenderer_R;//レイザーポインター左右
    [SerializeField] float StartWidth, EndWidth;//レイザーポインター左右　太さ
    [SerializeField] Text textbullet_countL, textbullet_countR;//仮の残弾数UI左右
    int bullet_countL, bullet_countR;//左右それぞれの残弾数
    int hit;//左右のヒットをそれぞれ判別する数字　左は0　右は1
    [SerializeField] GameObject Ob;
    void Start()
    {
        lineRenderer_L =LGun.GetComponent<LineRenderer>();
        lineRenderer_R = RGun.GetComponent<LineRenderer>();
        bullet_countL = 1;
        bullet_countR = 1;
        lineRenderer_L.startWidth = StartWidth;
        lineRenderer_L.endWidth = EndWidth;
        lineRenderer_R.startWidth = StartWidth;
        lineRenderer_R.endWidth = EndWidth;
    }

    // Update is called once per frame
    void Update()
    {
        textbullet_countL.text = bullet_countL.ToString();
        textbullet_countR.text = bullet_countR.ToString();
        lineRenderer_L.SetPosition(0, LGun_trans.position);
        lineRenderer_L.SetPosition(1, LGun_trajectory.position) ;
        lineRenderer_R.SetPosition(0, RGun_trans.position);
        lineRenderer_R.SetPosition(1, RGun_trajectory.position);
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))//左トリガーを押したとき
        {
            bullet_countL = 0;
            LGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))//左トリガーを戻したとき
        {
            LGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))//右トリガーを押したとき
        {
            bullet_countR = 0;
            RGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))//右トリガーを戻したとき
        {
            RGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        ////当たった的の種類を確認する用のスクリプト
        //Ray ray = new Ray(LGun_trans.position, LGun_trans.position - LGun_trajectory.position);
        //RaycastHit hit;
        //if(Physics.Raycast(ray,out hit, 10f))
        //{
        //    if (hit.collider.name == "Target")
        //    {
        //        Ob.SetActive(true);
        //        Debug.Log("UIを確認");
        //    }
        //    else Ob.SetActive(false);
        //    Debug.Log(hit.collider.gameObject.name);
        //}
        //Debug.DrawRay(LGun_trans.position, LGun_trajectory.position- LGun_trans.position, Color.red );


        //的に命中した際のリロード機能　今はXボタンで左ヒット　Aボタンで右ヒットを仮においている
        if (OVRInput.GetDown(OVRInput.RawButton.X)) Reload(0);//左当たった場合
        if (OVRInput.GetDown(OVRInput.RawButton.A)) Reload(1); //右当たった場合
        

    }
    public void Reload(int hit)
    {
        if(hit == 0) bullet_countL = 1;
        if (hit == 1) bullet_countR = 1;
    }
}


    

