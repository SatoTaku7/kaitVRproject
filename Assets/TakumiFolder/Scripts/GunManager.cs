using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour, IGunManager
{
    [SerializeField] GameObject LGun, RGun;
    [SerializeField] Transform LGun_trans, LGun_trajectory,LGun_Trigger;//���̏e�̈ʒu�E�O���ʒu�E�g���K�[
    [SerializeField] Transform RGun_trans, RGun_trajectory, RGun_Trigger;//�E�̏e�̈ʒu�E�O���ʒu�E�g���K�[
    LineRenderer lineRenderer_L,lineRenderer_R;//���C�U�[�|�C���^�[���E
    [SerializeField] float StartWidth, EndWidth;//���C�U�[�|�C���^�[���E�@����
    [SerializeField] Text textbullet_countL, textbullet_countR;//���̎c�e��UI���E
    private int bullet_countL, bullet_countR;//���E���ꂼ��̎c�e��
    private int hit;
    private int layerMask=1<<7|1<<6;
    [SerializeField] bool InfiniteMode;

    public  string ButtonName;//�N���b�N���ꂽ�{�^���̖��O���Q�Ɓ@oculus�W���̋@�\���g���Ȃ������̂ŕʂ̂����ő��
    public  bool ButtonClicked;//�{�^�����N���b�N���ꂽ���ǂ����@oculus�W���̋@�\���g���Ȃ������̂ŕʂ̂����ő��

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
        if (InfiniteMode == false)//�e�������[�h����Ȃ��Ƃ��͐�����\�L
        {
            textbullet_countL.text = bullet_countL.ToString();
            textbullet_countR.text = bullet_countR.ToString();
        }
        else
        {
            textbullet_countL.text = "��";
            textbullet_countR.text = "��";
        }
        
        lineRenderer_L.SetPosition(0, LGun_trans.position);
        lineRenderer_L.SetPosition(1, LGun_trajectory.position) ;
        lineRenderer_R.SetPosition(0, RGun_trans.position);
        lineRenderer_R.SetPosition(1, RGun_trajectory.position);
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))//���g���K�[���������Ƃ�
        {
            if (InfiniteMode == false)
            {
                bullet_countL = 0;
            }   
            LGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            Ray();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))//���g���K�[��߂����Ƃ�
        {
            LGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
            ButtonClicked = false;
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))//�E�g���K�[���������Ƃ�
        {
            if (InfiniteMode == false)
            {
                bullet_countL = 0;
            }
            RGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
            Ray();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))//�E�g���K�[��߂����Ƃ�
        {
            RGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
            ButtonClicked = false;
        } 
    }
    public void Ray()
    {
        //���������I�̎�ނ��m�F����p�̃X�N���v�g
        Ray ray_L = new Ray(LGun_trans.position, LGun_trajectory.position - LGun_trans.position);
        Ray ray_R = new Ray(RGun_trans.position, RGun_trajectory.position - RGun_trans.position);
        RaycastHit hitobj;
        if (Physics.Raycast(ray_L, out hitobj, 200, layerMask))
        {
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))//���g���K�[���������Ƃ�
            {
                if (hitobj.collider.gameObject.layer == 7)//UI�̃{�^���̎�
                {
                    ButtonName = hitobj.collider.gameObject.name;
                    ButtonClicked = true;
                    //Debug.Log("UI���m�F");
                }
                if (hitobj.collider.gameObject.layer == 6 && (bullet_countL == 1 || InfiniteMode == true))//�I�ɓ��������Ƃ�
                {
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(hit);
                    Reload();
                    Debug.Log(hitobj.collider.gameObject.name + ":�Փ˂����I�u�W�F�N�g");
                }
            }
            Debug.DrawRay(LGun_trans.position, LGun_trajectory.position - LGun_trans.position, Color.red);
            //Debug.Log(hitobj.collider.gameObject.name);
        }
        if (Physics.Raycast(ray_R, out hitobj, 200, layerMask))
        {
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))//�E�g���K�[���������Ƃ�
            {

                if (hitobj.collider.gameObject.layer == 7)//UI�̃{�^���̎�
                {
                    ButtonName = hitobj.collider.gameObject.name;
                    ButtonClicked = true;
                    //Debug.Log("UI���m�F");
                }
                if (hitobj.collider.gameObject.layer == 6 && (bullet_countR == 1 || InfiniteMode == true))//�I�ɓ��������Ƃ�
                {
                    hitobj.collider.gameObject.GetComponentInParent<IGunBreakTarget>().BreakTarget(hit);
                    Reload();
                    Debug.Log(hitobj.collider.gameObject.name + ":�Փ˂����I�u�W�F�N�g");
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


    

