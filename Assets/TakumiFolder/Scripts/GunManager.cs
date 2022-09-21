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
    int bullet_countL, bullet_countR;//���E���ꂼ��̎c�e��
    int hit;
    
    [SerializeField] GameObject Ob;
    
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
        Ob.SetActive(true);
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
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))//���g���K�[���������Ƃ�
        {
            bullet_countL = 0;
            LGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))//���g���K�[��߂����Ƃ�
        {
            LGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))//�E�g���K�[���������Ƃ�
        {
            bullet_countR = 0;
            RGun_Trigger.transform.localRotation = Quaternion.Euler(-27f, 0, 0);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))//�E�g���K�[��߂����Ƃ�
        {
            RGun_Trigger.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        //���������I�̎�ނ��m�F����p�̃X�N���v�g
        Ray ray = new Ray(LGun_trans.position, LGun_trajectory.position- LGun_trans.position);
        RaycastHit hitobj;
        if (Physics.Raycast(ray, out hitobj, 10f))
        {
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))//���g���K�[���������Ƃ�
            {
                if (hitobj.collider.name == "Button")
                {
                    Ob.SetActive(false);
                    Debug.Log("UI���m�F");
                }
            }
            
            //else Ob.SetActive(false);
            Debug.Log(hitobj.collider.gameObject.name);
        }
        Debug.DrawRay(LGun_trans.position, LGun_trajectory.position - LGun_trans.position, Color.red);


        //�I�ɖ��������ۂ̃����[�h�@�\�@����X�{�^���ō��q�b�g�@A�{�^���ŉE�q�b�g�����ɂ����Ă���
        if (OVRInput.GetDown(OVRInput.RawButton.X))//�����������ꍇ
        {
            hit = 1;
            Reload(); 
        }
        if (OVRInput.GetDown(OVRInput.RawButton.A)) //�E���������ꍇ
        {
            hit = 2;
            Reload(); 
        } 
        

    }
    public  void Reload()
    {
        if(hit == 1) bullet_countL = 1;
        if (hit == 2) bullet_countR = 1;
        hit = 0;
    }
    public void PowerUp()
    {

    }
    public void GameOver()
    {

    }
}


    

